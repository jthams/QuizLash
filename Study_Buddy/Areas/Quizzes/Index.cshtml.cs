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
    public class IndexModel : PageModel
    {
        private readonly Domain.DataContexts.ApplicationDataContext _context;

        public IndexModel(Domain.DataContexts.ApplicationDataContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get;set; }

        public async Task OnGetAsync()
        {
            Quiz = await _context.Quizs
                .Include(q => q.Topic).ToListAsync();
        }
    }
}
