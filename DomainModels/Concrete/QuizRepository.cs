using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Abstract;
using Domain.DataContexts;

namespace Domain.Concrete
{
    public class QuizRepository : IDataRepository<Quiz>
    {
        private readonly ApplicationDataContext _context;

        public IQueryable<Quiz> Items { get { return _context.Quizs; } }

        public IEnumerable<Topic> Topics { get { return _context.Topics; } }

        public IEnumerable<Question> Questions { get { return _context.Questions; } }

        public QuizRepository(ApplicationDataContext Context)
        {
            _context = Context;
        }

        public async Task<Quiz> FindAsync(int? Id)
        {
            return await _context.Quizs.FindAsync(Id);
        }

        public void Add(Quiz quiz)
        {
            _context.Add(quiz);
            _context.SaveChanges();
        }
        public void Update(Quiz quiz)
        {
            _context.Update(quiz);
            _context.SaveChanges();
        }
        public void Remove(Quiz quiz)
        {
            _context.Quizs.Remove(quiz);
            _context.SaveChanges();
        }
    }
}
