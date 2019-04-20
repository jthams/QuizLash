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
    public class QuestionRepository : IDataRepository<Question>, ITopicRepository
    {
        private readonly ApplicationDataContext _context;

        public IQueryable<Question> Items { get { return _context.Questions; } }

        public IEnumerable<Topic> Topics { get { return _context.Topics; } }

        public QuestionRepository(ApplicationDataContext Context)
        {
            _context = Context;
        }

        public async Task<Question> FindAsync(int? Id)
        {
            return await _context.Questions.FindAsync(Id);
        }
        
        public void Add(Question question)
        {
            _context.Add(question);
            _context.SaveChanges();
        }

        public void Update(Question question)
        {
            _context.Update(question);
            _context.SaveChanges();
        }

        public void Remove(Question question)
        {
            _context.Questions.Remove(question);
            _context.SaveChanges();
        }
        
        // return only topics that are referenced as FKs in the question table
        public IEnumerable<Topic> QuestionTopics()
        {
            return  _context.Topics.Join(_context.Questions,
                                          t => t.TopicID,
                                          q => q.TopicID,
                                          (t, q) => t).Distinct();
        }

        // Create a collection of questions specific to the topic
        public IQueryable<Question> SelectQuestionsForTopic(int TopicID)
        {
            return _context.Questions.Where(q => q.TopicID == TopicID);
        }

        // Create a collection of questions with Choices
        public IQueryable<Question> GetMCQuestions(int TopicID)
        {
            var questionPool = SelectQuestionsForTopic(TopicID);

            return questionPool.Join(_context.Choices, q => q.QuestionID, c => c.QuestionID, (q, c) => q);
        }
    }
}
