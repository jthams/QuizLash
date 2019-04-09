using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuizQuestionRelation
    {
        // Many to many
        public int? QuizID { get; set; }
        public Quiz Quiz { get; set; }

        // Many to many
        public int? QuestionID { get; set; }
        public Question Question { get; set; }

    }
}
