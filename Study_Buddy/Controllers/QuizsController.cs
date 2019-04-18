using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Concrete;
using Domain.Entities;
using Domain.Abstract;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    [Authorize]
    public class QuizsController : Controller
    {
        // Provide access to the data layer
        private readonly QuizRepository _quizData;

        private readonly QuestionRepository _questionData;

        // Access the user information
        private readonly UserManager<IdentityUser> _userManager;
        
        public QuizsController(IDataRepository<Quiz> QuizData, IDataRepository<Question> QuestionData, UserManager<IdentityUser> userManager)
        {

            _quizData = (QuizRepository)QuizData;
            _questionData = (QuestionRepository)QuestionData;
            _userManager = userManager;
        }

        //*********************** HELPER METHODS SECTION ****************************************************

        // Get the current user Id
        private string _currentUser { get { return _userManager.GetUserId(User); } }

        private IEnumerable<Topic> AvailableTopics { get { return _questionData.QuestionTopics(); } }
        

        // Create distinct collection of questions with the length of numberOfQuestions and the topic of the quiz
        private async Task<List<Question>> getUniqueQuestions(QuizViewModel quiz)
        {
            // Create a random object to seed the index reference
            Random rand = new Random();

            // A pool of questions to choose from
            List<Question> questionPool = _questionData.SelectQuestionsForTopic(quiz.TopicID).ToList();

            // A random collection of unique items
            var uniqueQuestions = questionPool.OrderBy(x => rand.Next()).Take(quiz.NumberOfQuestions).ToList();

            return uniqueQuestions;
        }

        // return a collection of unique questions that the user 
        private async Task<List<Question>> getUsersQuestions(QuizViewModel quiz)
        {
            // A pool of questions to choose from
            List<Question> questionPool = _questionData.SelectQuestionsForTopic(quiz.TopicID).ToList();

            // Collection of users questions
            List<Question> UsersQuestions = questionPool.Where(q => q.Creator == _currentUser).ToList();

            return UsersQuestions;
        }
        
        // Grading method for short answer quizzes
        private async Task<decimal> GradeSAQuiz(QuizViewModel quiz)
        {
            List<Question> questionPool = _questionData.SelectQuestionsForTopic(quiz.TopicID).ToList();
            decimal score = 0;
            decimal questionValue = 100 / quiz.NumberOfQuestions;

            // TODO: add more inclusive grading logic including accounting for acronyms
            foreach (var item in questionPool)
            {
                if (quiz.QidGuess.ContainsKey(item.QuestionID))
                {
                    if (item.Answer.ToLower().Contains(quiz.QidGuess[item.QuestionID].ToLower()))
                    {
                        score += questionValue;
                    }
                }
            }
            return score;
        }
        
        // Randomize the order in which the multiple choice options are presented
        public static string[] RandomizeOrder(string[] order)
         {
            Random rand = new Random();
            int pusher = rand.Next(0,4);

            for (int i = 0; i < 4; i++)
            {
                order[i] = order[pusher % 3];
            }
            return order;

         }

        //*********************** END OF HELPER METHODS SECTION ***********************

        [HttpGet] // GET: Flashcard / Create
        public async Task<IActionResult> CreateFlashcard()
        {
            // Populate a collection with the output from the linq statement and provide it to the select list
          
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description");

            // Object to bind to
            QuizViewModel quizVM = new QuizViewModel();

            return View(quizVM);
        }

        [HttpPost] // Populates the metadata to render the flashcards
        public async Task<IActionResult> CreateFlashcard(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description", quizVM.TopicID);

            // Use helper method to populate the questions
            var uniqueQuestions = await getUniqueQuestions(quizVM);
            var usersQuestions = await getUsersQuestions(quizVM);

            // Set the displayed questions based on the boolean from the user
            if (quizVM.PrivateSource)
            {
                quizVM.Questions = usersQuestions;
            }
            else
            {
                quizVM.Questions = uniqueQuestions;
            }
            return View("RenderFlashCard",quizVM);
        }

        [HttpGet]// GET: Quizs/Create
        public async Task<IActionResult> CreateQuiz()
        {
           
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description");

            // Object to bind to
            QuizViewModel quizVM = new QuizViewModel();

            // Set the Owner to the current user
            quizVM.Owner = _currentUser;

            return View(quizVM);
        }

        [HttpPost] // Populates the metadata to render the quiz
        public async Task<IActionResult> CreateQuiz(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selection
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description",quizVM.TopicID);

            // Create a dictionary to help grade the quiz on server side
            Dictionary<int, string> valuePairs = new Dictionary<int, string>();

            // Use helper method to populate the questions
            var uniqueQuestions = await getUniqueQuestions(quizVM);
            var usersQuestions = await getUsersQuestions(quizVM);

            // Set the displayed questions based on the boolean from the user
            // Populate the keys of the dictionary with the appropriate QuestionIDs
            if (quizVM.PrivateSource)
            {
                quizVM.Questions = usersQuestions;
                foreach (var item in usersQuestions)
                {
                    valuePairs.Add(item.QuestionID,null);
                }
            }
            else
            {
                quizVM.Questions = uniqueQuestions;
                foreach (var item in uniqueQuestions)
                {
                    valuePairs.Add(item.QuestionID, null);
                }
            }
            // set the VM property to the new dictionary
            quizVM.QidGuess = valuePairs;

            return  View("RenderSAQuiz",quizVM);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken] // Adds the quiz to the database
        public async Task<IActionResult> FinalizeQuiz(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic 
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description", quizVM.TopicID);

            // Grade the quiz
            decimal score = await GradeSAQuiz(quizVM);

            // Create instance of an object to bind to the Quiz object
            List<QuizQuestionRelation> quizQuestions = new List<QuizQuestionRelation>();
            
            // Use the dictionary to populate the composite table 
            foreach (var item in quizVM.QidGuess)
            {
                QuizQuestionRelation qqr = new QuizQuestionRelation
                {
                    QuestionID = item.Key
                };
                quizQuestions.Add(qqr);
            }

            // Set object property values to the metadata values
            Quiz Quiz = new Quiz();
            Quiz.TopicID = quizVM.TopicID;
            Quiz.Owner = quizVM.Owner;
            Quiz.NumberOfQuestions = quizVM.NumberOfQuestions;
            Quiz.Score = score;
            Quiz.QuizQuestionRelation = quizQuestions;

            if (ModelState.IsValid)
            {
                _quizData.Add(Quiz);
                
                return RedirectToAction("Index", "UserContent");
            }
            return View(quizVM);
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var applicationDataContext = _quizData.Items.Include(q => q.Topic);
            return View(await applicationDataContext.ToListAsync());
        }
        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _quizData.Items
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }
        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _quizData.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            ViewData["TopicID"] = new SelectList(_quizData.Topics, "TopicID", "TopicID", quiz.TopicID);
            return View(quiz);
        }
        [HttpPost] // POST: Quizs/Edit/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizID,Score,NumberOfQuestions,Owner,TopicID")] Quiz quiz)
        {
            if (id != quiz.QuizID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _quizData.Update(quiz);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "UserContent");
            }
            ViewData["TopicID"] = new SelectList(_quizData.Topics, "TopicID", "TopicID", quiz.TopicID);
            return View(quiz);
        }
        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _quizData.Items
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // POST: Quizs/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _quizData.FindAsync(id);
            _quizData.Remove(quiz);
      
            return RedirectToAction("Index", "UserContent");
        }
        private bool QuizExists(int id)
        {
            return _quizData.Items.Any(e => e.QuizID == id);
        }
    }
}
