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

        public IEnumerable<Topic> Topics { get { return _context.Topics; } }

        public IEnumerable<Question> GetUserQuestions(string UserId)
        {
            return _context.Questions.Where(q => q.Creator == UserId);
        }

        public IEnumerable<Quiz> GetUserQuizzes(string UserId)
        {
            return _context.Quizs.Where(q => q.Owner == UserId);
        }

        public Dictionary<int, decimal> GetTopicPerformance(string UserId)
        {
            Dictionary<int, decimal> TopicPerformance = new Dictionary<int, decimal>();

            List<KeyValuePair<int, decimal>> temp = new List<KeyValuePair<int, decimal>>();

            List<int> topicIDs = new List<int>();

            foreach (var item in GetUserQuizzes(UserId))
            {
                temp.Add(new KeyValuePair<int, decimal>(item.TopicID, item.Score));
                topicIDs.Add(item.TopicID);
            }

            foreach (var item in topicIDs.Distinct())
            {
                var average = temp.Where(t => t.Key == item).Average(t => t.Value);
                TopicPerformance.Add(item, average);
            }

            return TopicPerformance;

        }
        

    }
}
