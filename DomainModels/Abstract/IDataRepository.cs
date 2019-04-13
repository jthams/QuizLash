using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Text;

namespace Domain.Abstract
{
    public interface IDataRepository
    {
        IEnumerable<Question> Questions { get; }
        IEnumerable<Quiz> Quizzes { get; }
        IEnumerable<Topic> Topics { get; }
        IEnumerable<Choice> Choices { get; }
        IEnumerable<QuizQuestionRelation> QQRs { get; }
    }
}
