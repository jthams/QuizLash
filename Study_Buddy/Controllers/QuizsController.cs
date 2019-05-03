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

        // Access the user information
        private readonly UserManager<IdentityUser> _userManager;
        
        public QuizsController(IDataRepository<Quiz> QuizData, UserManager<IdentityUser> userManager)
        {
            _quizData = (QuizRepository)QuizData;
            _userManager = userManager;
        }

        //*********************** HELPER METHODS SECTION ****************************************************

        // Get the current user Id
        private string _currentUser { get { return _userManager.GetUserId(User); } }

        // Populate a collection with the output from DAL method
        private IEnumerable<Topic> _availableTopics { get { return _quizData.QuestionTopics(); } }
        
        // Create distinct collection of questions with the length of numberOfQuestions and the topic of the quiz
        private List<Question> getUniqueQuestions(QuizViewModel quiz)
        {
            Random rand = new Random();
            List<Question> questionPool =_quizData.SelectQuestionsForTopic(quiz.TopicID).ToList();
            var uniqueQuestions = questionPool.OrderBy(x => rand.Next()).Take(quiz.NumberOfQuestions).ToList();

            return uniqueQuestions;
        }

        // Return a collection of unique questions that the user 
        private List<Question> getUsersQuestions(QuizViewModel quiz)
        {
            List<Question> questionPool =_quizData.SelectQuestionsForTopic(quiz.TopicID).ToList();
            List<Question> UsersQuestions = questionPool.Where(q => q.Creator == _currentUser).ToList();

            return UsersQuestions;
        }

        // Set the collection for questions in the quizVM
        public QuizViewModel SetQuestionsFlashCards(QuizViewModel quizVM)
        {
            // Set the displayed questions based on the boolean from the user
            if (quizVM.PrivateSource)
            {
                quizVM.Questions = getUsersQuestions(quizVM);
            }
            else
            {
                quizVM.Questions = getUniqueQuestions(quizVM);
            }

            return quizVM;
        }
        // Set Multiple Choice Questions
        public QuizViewModel SetQuestionsMCQuiz(QuizViewModel quizVM)
        {
            // List to set the view model property to
            List<MCSelection> selections = new List<MCSelection>();
            Dictionary<int, string> valuePairs = new Dictionary<int, string>();

            // Set the displayed questions based on the boolean from the user
            if (quizVM.PrivateSource)
            {
                quizVM.Questions = getUsersQuestions(quizVM);
                foreach (var item in quizVM.Questions)
                {
                    valuePairs.Add(item.QuestionID, null);
                    var textVal = MCRandomAppearance(item).ToArray();
                    selections.Add
                        (
                            new MCSelection
                            {
                                QuestionId = item.QuestionID,
                                Choice1 = textVal[0],
                                Choice2 = textVal[1],
                                Choice3 = textVal[2],
                                Choice4 = textVal[3]
                            }
                        );
                }
            }
            else
            {
                quizVM.Questions = getUniqueQuestions(quizVM);
                foreach (var item in quizVM.Questions)
                {
                    valuePairs.Add(item.QuestionID, null);
                    var textVal = MCRandomAppearance(item).ToArray();
                    selections.Add
                        (
                            new MCSelection
                            {
                                QuestionId = item.QuestionID,
                                Choice1 = textVal[0],
                                Choice2 = textVal[1],
                                Choice3 = textVal[2],
                                Choice4 = textVal[3]
                            }
                        );
                }
            }
            quizVM.QidGuess = valuePairs;
            quizVM.Selections = selections;

            return quizVM;
        }
        // Set short answer questions
        public QuizViewModel SetQuestionsSAQuiz(QuizViewModel quizVM)
        {
            // Create a dictionary to help grade the quiz on server side
            Dictionary<int, string> valuePairs = new Dictionary<int, string>();

            // Set the displayed questions based on the boolean from the user
            // Populate the keys of the dictionary with the appropriate QuestionIDs
            if (quizVM.PrivateSource)
            {
                quizVM.Questions = getUsersQuestions(quizVM);
                foreach (var item in quizVM.Questions)
                {
                    valuePairs.Add(item.QuestionID, null);
                }
            }
            else
            {
                quizVM.Questions = getUniqueQuestions(quizVM);
                foreach (var item in quizVM.Questions)
                {
                    valuePairs.Add(item.QuestionID, null);
                }
            }
            // set the VM property to the new dictionary
            quizVM.QidGuess = valuePairs;

            return quizVM;
        }
        
        private decimal GradeQuiz(QuizViewModel quiz)
        {
            List<Question> questionPool =_quizData.SelectQuestionsForTopic(quiz.TopicID).ToList();
            decimal score = 0;
            decimal questionValue = 100 / quiz.QidGuess.Count; 

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
        // Create the options for a MC Question
        private IEnumerable<string> MCRandomAppearance(Question question)
        {
            List<string> randChoices = new List<string>();
            randChoices.Add(question.Answer);

            List<string> choiceText = _quizData.Choices.Where(c => c.QuestionID == question.QuestionID).Select(c => c.Text).ToList();
            foreach (var item in choiceText)
            {
                randChoices.Add(item);
            }

            IEnumerable<string> output = randChoices.OrderBy(x => x.Length).ToList();

            return output;
        }

        //*********************** END OF HELPER METHODS SECTION ***********************

        [HttpGet] // GET: Flashcard / Create
        public IActionResult CreateFlashcard()
        {
            // Populate a collection with the output from the linq statement and provide it to the select list
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "Description");
            QuizViewModel quizVM = new QuizViewModel();

            return View(quizVM);
        }

        [HttpPost] // Populates the metadata to render the flashcards
        public IActionResult CreateFlashcard(QuizViewModel quizVM)
        {
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "Description", quizVM.TopicID);

            QuizViewModel QuizVM = SetQuestionsFlashCards(quizVM);

            return View("RenderFlashCard",QuizVM);
        }

       

        [HttpGet]// GET: Quizs/Create
        public IActionResult CreateQuiz()
        {

            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "Description");
            
            QuizViewModel quizVM = new QuizViewModel();

            // Set the Owner to the current user
            quizVM.Owner = _currentUser;

            return View(quizVM);
        }

        [HttpPost] // Populates the metadata to render the quiz
        public IActionResult CreateQuiz(QuizViewModel quizVM)
        {
            // Maintain selection state
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "Description",quizVM.TopicID);
            switch (quizVM.Type)
            {
                case "MC":
                    QuizViewModel QuizVmMC = SetQuestionsMCQuiz(quizVM);
                    return View("RenderMCQuiz", QuizVmMC);
                    
                case "SA":
                    QuizViewModel QuizVmSA = SetQuestionsSAQuiz(quizVM);
                    return View("RenderSAQuiz", QuizVmSA);

                case "TF":
                    QuizViewModel QuizVmTF = SetQuestionsSAQuiz(quizVM);
                    return View("RenderSAQuiz", QuizVmTF);
            }

            return  View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken] // Adds the quiz to the database
        public IActionResult FinalizeQuiz(QuizViewModel quizVM)
        {
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "Description", quizVM.TopicID);
            decimal score = GradeQuiz(quizVM);

            // Create a collection to bind the composite table
            List<QuizQuestionRelation> quizQuestions = new List<QuizQuestionRelation>();
            
            // Use the dictionary to populate the collection
            foreach (var item in quizVM.QidGuess)
            {
                QuizQuestionRelation qqr = new QuizQuestionRelation
                {
                    QuestionID = item.Key
                };
                quizQuestions.Add(qqr);
            }

            // Set object property values to the metadata values
            Quiz Quiz = new Quiz
            {
                TopicID = quizVM.TopicID,
                Owner = quizVM.Owner,
                NumberOfQuestions = quizVM.NumberOfQuestions,
                Score = score,
                QuizQuestionRelation = quizQuestions
            };
            
            if (ModelState.IsValid)
            {
                _quizData.Add(Quiz);
                
                return RedirectToAction("Index", "UserAccount");
            }

            return View(quizVM);
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            var quizzes = _quizData.Items.Include(q => q.Topic);
            return View(await quizzes.ToListAsync());
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
            QuizViewModel QVM = new QuizViewModel
            {
                QuizID = quiz.QuizID,
                NumberOfQuestions = quiz.NumberOfQuestions,
                TopicDescription = quiz.Topic.Description,
                Score = quiz.Score,
                Questions = _quizData.QuizQuestions(quiz.QuizID).ToList()
            };

            return View(QVM);
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
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "TopicID", quiz.TopicID);
            return View(quiz);
        }

        [HttpPost] // POST: Quizs/Edit/5
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("QuizID,Score,NumberOfQuestions,Owner,TopicID")] Quiz quiz)
        {
            ViewData["TopicID"] = new SelectList(_availableTopics, "TopicID", "TopicID", quiz.TopicID);
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

                return RedirectToAction("Index", "UserAccount");
            }
            
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
      
            return RedirectToAction("Index", "UserAccount");
        }
        private bool QuizExists(int id)
        {
            return _quizData.Items.Any(e => e.QuizID == id);
        }
    }
}
