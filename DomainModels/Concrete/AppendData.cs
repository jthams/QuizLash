using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Abstract;
using Domain.Entities;
using Domain.DataContexts;


namespace Domain.Concrete
{
    public class AppendData 
    {
        private readonly ApplicationDataContext _context = new ApplicationDataContext();

        public async void ProcessQuizAsync(Quiz quiz)
        {
            _context.Add(quiz);
            await _context.SaveChangesAsync();
        }
        public async void ProcessQuestionAsync(Question question)
        {
            _context.Add(question);
            await _context.SaveChangesAsync();
        }
        public async void ProcessTopicAsync(Topic topic)
        {
            _context.Add(topic);
            await _context.SaveChangesAsync();
        }
        public async void ProcessQQRAsync(QuizQuestionRelation qqr)
        {
            _context.Add(qqr);
            await _context.SaveChangesAsync();
        }
    }
}
