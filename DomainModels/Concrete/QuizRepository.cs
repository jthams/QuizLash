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
    public class QuizRepository : QuestionRepository, IDataRepository<Quiz>
    {
        private readonly ApplicationDataContext _context;

        new public IQueryable<Quiz> Items { get { return _context.Quizs; } }

        public IQueryable<Question> GetQuestions => base.Items;

        new public IEnumerable<Topic> Topics { get { return _context.Topics; } }

        public QuizRepository(ApplicationDataContext Context) : base(Context)
        {
            _context = Context;
        }

        new public async Task<Quiz> FindAsync(int? Id)
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

        public IEnumerable<Question> QuizQuestions(int id)
        {
            var Qids = _context.QuizQuestionRelation.Where(x => x.QuizID == id).Select(x => x.QuestionID);
            IEnumerable<Question> quizQuestions = Qids.Join(_context.Questions,
                                                    i => i.Value,
                                                    q => q.QuestionID,
                                                    (i, q) => q);
            return quizQuestions;
        }
    }
}
