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
    public class UserDataRepository : IUserDataRepository
    {
        private readonly ApplicationDataContext _context;
        public UserDataRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Question> GetUserQuestions(string UserId)
        {
            return _context.Questions.Where(q => q.Creator == UserId);
        }

        public IEnumerable<Quiz> GetUserQuizzes(string UserId)
        {
            return _context.Quizs.Where(q => q.Owner == UserId);
        }

        public IEnumerable<Topic> Topics { get { return _context.Topics; } }

    }
}
