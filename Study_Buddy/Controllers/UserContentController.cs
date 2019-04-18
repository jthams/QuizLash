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
using Domain.DataContexts;
using WebUI.ViewModels;


namespace WebUI.Controllers
{
    [Authorize]
    public class UserContentController : Controller
    {
        private readonly IUserDataRepository _userData;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Dictionary<int, string> _topics = new Dictionary<int, string>();
        
        // Access the data and the user manager with dependency injection
        public UserContentController(IUserDataRepository UserData, UserManager<IdentityUser> userManager)
        {
            _userData = UserData;
            _userManager = userManager;
        }

        // Get the current users ID
        private string _currentUser => _userManager.GetUserId(User);
       
        private void setTopicDictionary()
        {
            foreach (var item in _userData.Topics)
            {
                _topics.TryAdd(item.TopicID, item.Description);
            }
        }

        // GET: UserContent
        public ActionResult Index()
        {
            setTopicDictionary();
            // Create a new object for the composite Model
            UserContentViewModel UserModel = new UserContentViewModel();

            // Set the model properties to the output of the helper methods
            UserModel.Quizzes = _userData.GetUserQuizzes(_currentUser);
            UserModel.Questions = _userData.GetUserQuestions(_currentUser);

            // Return the data to the view to be displayed
            return View(UserModel);
        }

       


    }
}