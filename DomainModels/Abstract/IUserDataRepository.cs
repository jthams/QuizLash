using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System.Text;

namespace Domain.Abstract
{
    public interface IUserDataRepository
    {
        IEnumerable<Topic> Topics { get; }
        IEnumerable<Quiz> GetUserQuizzes(string userID);
        IEnumerable<Question> GetUserQuestions(string userID);
    }
}
