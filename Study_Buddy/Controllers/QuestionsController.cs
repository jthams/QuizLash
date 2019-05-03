using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Entities;
using Domain.Abstract;
using Domain.Concrete;
using WebUI.ViewModels;


namespace WebUI.Controllers
{   
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly QuestionRepository _questionData;
        private readonly UserManager<IdentityUser> _userManager;

        public QuestionsController(IDataRepository<Question> QuestionData,UserManager<IdentityUser> userManager)
        {
            _questionData = (QuestionRepository)QuestionData;
            _userManager = userManager;
        }

        // Get the current user Id
        private string _currentUser { get { return _userManager.GetUserId(User); } }

        // Populate a collection with the output from DAL method
        private IEnumerable<Topic> Topics { get { return _questionData.Topics; } }

        
        // GET: Questions/Create
        public IActionResult Create()
        {
            // Get the list of topics for select topic option
            ViewData["TopicID"] = new SelectList(Topics, "TopicID", "Description");

            // Set the Creator of the question to the current user
            QuestionInputViewModel questionVM = new QuestionInputViewModel
            {
                Creator = _currentUser
            };
            
            return View(questionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QuestionInputViewModel questionVM)
        {
            ViewData["TopicID"] = new SelectList(Topics, "TopicID", "Description", questionVM.TopicID);
            
            // Set the text for choices to a list of strings
            List<string> choicesText = new List<string>()
            {
                {questionVM.Choice1},
                {questionVM.Choice2},
                {questionVM.Choice3}
            };
           
            // A container for the choice objects
            List<Choice> choices = new List<Choice>();
            foreach (var item in choicesText)
            {
                // Create the choice objects 
                Choice choice = new Choice
                {
                    Text = item
                };
                choices.Add(choice);
            }

            // Create a new entity object and set the entities properties to the values in the ViewModel
            Question question = new Question
            {
                Creator = questionVM.Creator,
                TopicID = questionVM.TopicID,
                Body = questionVM.Body,
                Answer = questionVM.Answer,
                Choices = choices
            };

            if (ModelState.IsValid)
            {
                _questionData.Add(question);

                return RedirectToAction("Index", "UserAccount");
            }
            
            return View(questionVM);
        }
        // GET: Questions
        public IActionResult Index()
        {
            return View(_questionData.Items);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _questionData.Items
                .Include(q => q.Topic)
            
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _questionData.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["TopicID"] = new SelectList(Topics, "TopicID", "Description", question.TopicID);
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("QuestionID,Body,Answer,TopicID,Creator")] Question question)
        {   
            if (id != question.QuestionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _questionData.Update(question);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["TopicID"] = new SelectList(Topics, "TopicID", "Description", question.TopicID);
                return RedirectToAction("Index", "UserAccount");
            }
            
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _questionData.Items
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _questionData.FindAsync(id);
            _questionData.Remove(question);
            return RedirectToAction("Index", "UserAccount");
        }

        private bool QuestionExists(int id)
        {
            return _questionData.Items.Any(e => e.QuestionID == id);
        }
    }
}
