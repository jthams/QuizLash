using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IQuizQuestionRelationRepository
    {
        IEnumerable<QuizQuestionRelation> QQRs { get; }
    }
}
