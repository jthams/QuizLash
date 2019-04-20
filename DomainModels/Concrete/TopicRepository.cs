using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Abstract;
using Domain.DataContexts;

namespace Domain.Concrete
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ApplicationDataContext _context;
        public TopicRepository(ApplicationDataContext Context)
        {
            _context = Context;
        }
        public IEnumerable<Topic> Topics { get { return _context.Topics; } }
    }
}
