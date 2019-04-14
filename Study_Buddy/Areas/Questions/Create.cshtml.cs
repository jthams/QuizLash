using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.DataContexts;
using Domain.Entities;

namespace WebUI.Areas.Questions
{
    public class CreateModel : PageModel
    {
        private readonly Domain.DataContexts.ApplicationDataContext _context;

        public CreateModel(Domain.DataContexts.ApplicationDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicID");
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}