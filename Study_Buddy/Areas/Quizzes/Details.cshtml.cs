using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.DataContexts;
using Domain.Entities;

namespace WebUI.Areas.Quizzes
{
    public class DetailsModel : PageModel
    {
        private readonly Domain.DataContexts.ApplicationDataContext _context;

        public DetailsModel(Domain.DataContexts.ApplicationDataContext context)
        {
            _context = context;
        }

        public Quiz Quiz { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quiz = await _context.Quizs
                .Include(q => q.Topic).FirstOrDefaultAsync(m => m.QuizID == id);

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
