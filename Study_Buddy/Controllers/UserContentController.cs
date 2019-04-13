using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.DataContexts;
using Domain.Entities;
using WebUI.ViewModels;


namespace WebUI.Controllers
{
    public class UserContentController : Controller
    {
        private readonly ApplicationDataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Dictionary<int, string> _topics = new Dictionary<int, string>();
        // Access the DBContext and the user manager with dependancy injection
        public UserContentController(ApplicationDataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Get the current users ID
        private string _currentUser => _userManager.GetUserId(User);

        // GET: UserContent
        public async Task<ActionResult> Index()
        {
            // Create a new object for the composite Model
            UserContentViewModel UserModel = new UserContentViewModel();

            foreach (var item in _context.Topics)
            {
                _topics.TryAdd(item.TopicID, item.Description);
            }
            // Set the model properties to the output of the helper methods
            UserModel.Quizzes = await GetQuizzesAsync();
            UserModel.Questions = await GetQuestionsAsync();

            // Return the data to the view to be displayed
            return View(UserModel);
        }

        // Get the quizzes specific to the current user
        private async Task<IEnumerable<Quiz>> GetQuizzesAsync()
        {
            var quizzes = await _context.Quizs.Where(q => q.Owner == _currentUser).ToListAsync();

            return quizzes;
        }

        // Get the questions specific to the current user
        private async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var questions = await _context.Questions.Where(q => q.Creator == _currentUser).ToListAsync();

            return questions;
        }


    }
}