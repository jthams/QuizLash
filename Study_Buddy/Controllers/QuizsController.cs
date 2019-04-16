using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DataContexts;
using Domain.Entities;
using Domain.Concrete;
using Domain.Abstract;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class QuizsController : Controller
    {
        // Provide access to the data layer
        private readonly ApplicationDataContext _context;

        // Access the user information
        private readonly UserManager<IdentityUser> _userManager;
        
        public QuizsController(ApplicationDataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string _currentUser => _userManager.GetUserId(User);

        //*********************** HELPER METHODS SECTION ***********************
        private async Task<IEnumerable<Topic>> getAvailableTopics()
        {
            // return only topics that are referenced as FKs in the question table
            var availableTopics = await _context.Topics.Join(_context.Questions,
                                          t => t.TopicID,
                                          q => q.TopicID,
                                          (t, q) => t).Distinct().ToListAsync();
            return availableTopics;
        }
        
        // Create a collection of questions specific to the topic of the quiz
        private async Task<List<Question>> selectQuestionsForTopic(QuizViewModel quiz)
        {
            var topicQuestions = await _context.Questions.Where(q => q.TopicID == quiz.TopicID).ToListAsync();

            return topicQuestions;
        }
        // Create distinct collection of questions with the length of numberOfQuestions and the topic of the quiz
        private async Task<List<Question>> getUniqueQuestions(QuizViewModel quiz)
        {
            // Create a random object to seed the index reference
            Random rand = new Random();

            // A pool of questions to choose from
            List<Question> questionPool = await selectQuestionsForTopic(quiz);

            // A random collection of unique items
            var uniqueQuestions = questionPool.OrderBy(x => rand.Next()).Take(quiz.NumberOfQuestions).ToList();

            return uniqueQuestions;
        }

        // return a collection of unique questions that the user 
        private async Task<List<Question>> getUsersQuestions(QuizViewModel quiz)
        {
            // A pool of questions to choose from
            List<Question> questionPool = await selectQuestionsForTopic(quiz);

            // Collection of users questions
            List<Question> UsersQuestions = questionPool.Where(q => q.Creator == _currentUser).ToList();

            return UsersQuestions;
        }

        private async Task<decimal> GradeSAQuiz(QuizViewModel quiz)
        {
            List<Question> questionPool = await selectQuestionsForTopic(quiz);
            decimal score = 0;
            decimal questionValue = 100 / quiz.NumberOfQuestions;

            foreach (var item in questionPool)
            {
                if (quiz.QidGuess.ContainsKey(item.QuestionID))
                {
                    if (item.Answer.Contains(quiz.QidGuess[item.QuestionID]))
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


        // GET: Flashcard / Create
        [HttpGet]
        public async Task<IActionResult> CreateFlashcard()
        {
            // Populate a collection with the output from the linq statement
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();

            // Object to bind to
            QuizViewModel quizVM = new QuizViewModel();

            // Dropdown list for the available topics
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description");

            // Pass the object with the owner bound
            return View(quizVM);
        }

        // Populates the metadata to render the flashcards
        [HttpPost]
        public async Task<IActionResult> CreateFlashcard(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description", quizVM.TopicID);

            // Use helper method to populate the questions
            var uniqueQuestions = await getUniqueQuestions(quizVM);
            var usersQuestions = await getUsersQuestions(quizVM);

           
            // Set the displayed questions based on the boolean from the user
            if (quizVM.privateSource)
            {
                quizVM.Questions = usersQuestions;
            }
            else
            {
                quizVM.Questions = uniqueQuestions;
            }

            // Pass the object with the owner bound
            return View("RenderFlashCard",quizVM);
        }
        // GET: Quizs/Create
        [HttpGet]
        public async Task<IActionResult> CreateQuiz()
        {
             
            // Populate a collection with the output from the linq statement
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();

            // Object to bind to
            QuizViewModel quizVM = new QuizViewModel();

            // Set the Owner to the current user
            quizVM.Owner = _currentUser;

            // Dropdown list for the available topics
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description");

            // Pass the object with the owner bound
             return View(quizVM);
        }

        // Populates the metadata to render the quiz
        [HttpPost]
        public async Task<IActionResult> CreateQuiz(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description",quizVM.TopicID);

            // Create a dictionary to help grade the quiz on server side
            Dictionary<int, string> valuePairs = new Dictionary<int, string>();

            // Use helper method to populate the questions
            var uniqueQuestions = await getUniqueQuestions(quizVM);
            var usersQuestions = await getUsersQuestions(quizVM);


            // Set the displayed questions based on the boolean from the user
            // Populate the keys of the dictionary with the appropriate QuestionIDs
            if (quizVM.privateSource)
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

            // Pass the object with the owner bound
            return  View("RenderSAQuiz",quizVM);
        }

        // Adds the quiz to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeQuiz(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description", quizVM.TopicID);

            // Grade the quiz
            decimal score = await GradeSAQuiz(quizVM);

            // Create instance of an object to bind to the Quiz object
            List<QuizQuestionRelation> quizQuestions = new List<QuizQuestionRelation>();

           
          
            
                

            // Set object property values to the metadata values
            Quiz Quiz = new Quiz();
            Quiz.TopicID = quizVM.TopicID;
            Quiz.Owner = quizVM.Owner;
            Quiz.NumberOfQuestions = quizVM.NumberOfQuestions;
            Quiz.Score = score;
            Quiz.QuizQuestionRelation = quizQuestions;

            if (ModelState.IsValid)
            {
                _context.Add(Quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserContent");
            }
            return View(quizVM);
        }

        
        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var applicationDataContext = _context.Quizs.Include(q => q.Topic);
            return View(await applicationDataContext.ToListAsync());
        }
        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizs
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

            var quiz = await _context.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID", quiz.TopicID);
            return View(quiz);
        }
        // POST: Quizs/Edit/5
        [HttpPost]
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
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID", quiz.TopicID);
            return View(quiz);
        }
        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizs
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }
        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quizs.FindAsync(id);
            _context.Quizs.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool QuizExists(int id)
        {
            return _context.Quizs.Any(e => e.QuizID == id);
        }
    }
}
