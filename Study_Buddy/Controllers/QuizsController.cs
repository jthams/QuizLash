using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Data;
using Domain.Entities;

namespace WebUI.Controllers
{
    public class QuizsController : Controller
    {
        private readonly ApplicationDataContext _context;

        public QuizsController(ApplicationDataContext context)
        {
            _context = context;
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

        // GET: Quizs/Create
        public IActionResult Create()
        {
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID");
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizID,Score,Owner,TopicID")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID", quiz.TopicID);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizID,Score,Owner,TopicID")] Quiz quiz)
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
