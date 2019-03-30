using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Question = new HashSet<Question>();
        }

        public int QuestiontypeId { get; set; }
        public string TypeDesc { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
