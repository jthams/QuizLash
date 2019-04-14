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

        
        List<Question> _questions { get; set; }
        
        
        public QuizsController(ApplicationDataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //*********************** HELPER METHODS SECTION ***********************
        private async Task<IEnumerable<Topic>> getAvailableTopics()
        {
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

        private async Task<IEnumerable<Question>> getUniqueQuestions(QuizViewModel quiz)
        {
            // Create a random object to seed the index reference
            Random rand = new Random();

            // Get a pool of questions to choose from
            List<Question> questionPool = await selectQuestionsForTopic(quiz);

            // Get a random collection of unique items
            var uniqueQuestions = questionPool.OrderBy(x => rand.Next()).Take(quiz.NumberOfQuestions);

            return uniqueQuestions;
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


        // GET: Quizs/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Populate a collection with the output from the linq statement
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();

            // Object to bind to
            QuizViewModel quizVM = new QuizViewModel();

            // Set the Owner to the current user
            quizVM.Owner = _userManager.GetUserId(User);

            // Dropdown list for the available topics
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description");

            // Pass the object with the owner bound
             return View(quizVM);
        }

        // Populates the metadata to render the quiz
        [HttpPost]
        public async Task<IActionResult> Create(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description",quizVM.TopicID);

            // Use helper method to populate the questions
            var uniqueQuestions = await getUniqueQuestions(quizVM);

            List<QuestionViewModel> questions = new List<QuestionViewModel>();

            foreach (var item in uniqueQuestions)
            {
                QuestionViewModel qvm = new QuestionViewModel();
                qvm.Body = item.Body;
                qvm.Answer = item.Answer;
                questions.Add(qvm);
            }
            quizVM.Questions = questions;

            // Pass the object with the owner bound
            return  View("Render",quizVM);
        }

        // Adds the quiz to the database
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalize(QuizViewModel quizVM)
        {
            // Populate a collection with the output from the linq statement and Maintain selected topic value
            IEnumerable<Topic> AvailableTopics = await getAvailableTopics();
            ViewData["TopicID"] = new SelectList(AvailableTopics, "TopicID", "Description", quizVM.TopicID);
            
            // Create instance of an object to bind to the Quiz object
            List<QuizQuestionRelation> quizQuestions = new List<QuizQuestionRelation>();

            // Running total
            decimal score = 0;

            // value to add to score
            decimal questionValue = 100 / quizVM.NumberOfQuestions;


            // Populate the many to many composite table
            foreach (var item in quizVM.Questions)
            {
                // Disable case sensitivity and accomdate for extra words
                if (item.Answer.ToLower().Contains(item.Guess.ToLower()))
                {
                    score += questionValue;
                }
                QuizQuestionRelation quizQuestionRelation = new QuizQuestionRelation();
                quizQuestionRelation.QuestionID = item.QuestionID;
                quizQuestions.Add(quizQuestionRelation);
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
                _context.Add(Quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserContent");
            }
            return View(Quiz);
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
