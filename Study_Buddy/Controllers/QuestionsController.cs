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
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDataContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public QuestionsController(ApplicationDataContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            // Get the list of topics for select topic option
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "Description");

            // Set the Creator of the question to the current user
            QuestionViewModel questionVM = new QuestionViewModel
            {
                Creator = _userManager.GetUserId(User)
            };
            
            return View(questionVM);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionViewModel questionVM)
        {
            // Set the text for choices to a list of strings
            List<string> choicesText = new List<string>();
            choicesText.Add(questionVM.Choice1);
            choicesText.Add(questionVM.Choice2);
            choicesText.Add(questionVM.Choice3);

            // A container for the choice objects
            List<Choice> choices = new List<Choice>();

            // Iterate through the values from the view
            foreach (var item in choicesText)
            {
                // Create the choice objects 
                Choice choice = new Choice();
                
                // Set the choice text property to the values of the list of strings
                choice.Text = item;
                choices.Add(choice);
            }

            // Create a new entity object 
            Question question = new Question();

            // Set the entities properties to the values in the ViewModel
            question.Creator = questionVM.Creator;
            question.TopicID = questionVM.TopicID;
            question.Body = questionVM.Body;
            question.Answer = questionVM.Answer;

            // Set Choices collection to the container created above
            question.Choices = choices;

            if (ModelState.IsValid)
            {   
                // Add the new object to the database
                _context.Add(question);
                await _context.SaveChangesAsync();

                //Return to the users view content after the quiz is added
                return RedirectToAction("Index", "UserContent");
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "Description", questionVM.TopicID);
            return View(questionVM);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID", question.TopicID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionID,Body,Answer,TopicID,Creator")] Question question)
        {
            if (id != question.QuestionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID", question.TopicID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
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
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionID == id);
        }
    }
}
