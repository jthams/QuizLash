using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.DataContexts;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Concrete
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDataContext _context;

        public IEnumerable<Topic> Topics
        {
            get { return _context.Topics; }
        }
        public IEnumerable<Quiz> Quizzes
        {
            get { return _context.Quizs; }
        }
        public IEnumerable<Question> Questions
        {
            get { return _context.Questions; }
        }
        public IEnumerable<Choice> Choices
        {
            get { return _context.Choices; }
        }
        public IEnumerable<QuizQuestionRelation> QQRs
        {
            get { return _context.QuizQuestionRelation; }
        }
        

       
    }
}
