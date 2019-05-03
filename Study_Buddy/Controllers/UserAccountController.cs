using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Domain.Abstract;
using WebUI.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebUI.Controllers
{

    public class UserAccountController : Controller
    {
        private readonly IUserDataRepository _userData;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAccountViewModel> _logger;
        private readonly Dictionary<int, string> _topics = new Dictionary<int, string>();

        // Access the data and the user manager with dependency injection
        public UserAccountController(IUserDataRepository userData, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserAccountViewModel> logger)
        {
            _userData = userData;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // Get the current users ID
        private string _currentUser { get { return _userManager.GetUserId(User); } }

        private void setTopicDictionary()
        {
            foreach (var item in _userData.Topics)
            {
                _topics.TryAdd(item.TopicID, item.Description);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            setTopicDictionary();
            UserAccountViewModel UserModel = new UserAccountViewModel
            {
                Quizzes = _userData.GetUserQuizzes(_currentUser),
                Questions = _userData.GetUserQuestions(_currentUser),
                TopicAverages = _userData.GetTopicPerformance(_currentUser)
            };

            return View(UserModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            UserAccountViewModel Avm = new UserAccountViewModel();

            return View(Avm);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([Bind("Email, Password")] UserAccountViewModel Input)
        {
            UserAccountViewModel model = new UserAccountViewModel();
            List<IdentityError> InputErrors = new List<IdentityError>();

            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _signInManager.PasswordSignInAsync(user, Input.Password, isPersistent: false, lockoutOnFailure: true);
                return LocalRedirect("/UserAccount/Index");
            }
            foreach (var error in result.Errors)
            {
                InputErrors.Add(error);
            }
            model.Errors = InputErrors;
            // If we got this far, something failed, redisplay form
            return View("_AccountError", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            UserAccountViewModel model = new UserAccountViewModel();

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email,Password,RememberMe")]UserAccountViewModel viewModel)
        {
            string returnUrl = Url.Content("~/");
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect("/UserAccount/Index");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = viewModel.RememberMe });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return PartialView("_LoginForm");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLogin()
        {
            return RedirectToAction("Login");
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            //var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var redirectUrl = Url.Action("ExternalCallback","UserAccount", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [HttpGet]
        public async Task<IActionResult> ExternalCallback(string returnUrl = null, string remoteError = null)
        {
            UserAccountViewModel model = new UserAccountViewModel();

            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                model.ErrorMessage = $"Error from external provider: {remoteError}";
                return View("/_AccountError", model);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                model.ErrorMessage = "Error loading external login information.";
                return View("/_AccountError", model);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return View("Index", model);
            }
            if (result.IsLockedOut)
            {
                return LocalRedirect("/Identity/Account/Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                model.ReturnUrl = returnUrl;
                model.LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    model.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ExternalCallback(UserAccountViewModel model)
        {
            List<IdentityError> InputErrors = new List<IdentityError>();
            UserAccountViewModel modelErrors = new UserAccountViewModel();

            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            model.LoginProvider = info.LoginProvider;
            if (info == null)
            {
                model.ErrorMessage = "Error loading external login information during confirmation.";
                return View("_AccountError",model);
            }
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                    return View("Index", model);
                }
            }
            foreach (var error in result.Errors)
            {
                InputErrors.Add(error);
            }
            modelErrors.Errors = InputErrors;

            return View("_AccountError", modelErrors);
        }
    }
}