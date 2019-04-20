using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System.Text;

namespace Domain.Abstract
{
    public interface IUserDataRepository : ITopicRepository
    {
        IEnumerable<Quiz> GetUserQuizzes(string userID);
        IEnumerable<Question> GetUserQuestions(string userID);
        Dictionary<int, decimal> GetTopicPerformance(string UserId);
    }
}
