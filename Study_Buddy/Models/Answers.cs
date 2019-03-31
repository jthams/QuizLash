using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Answers
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int? QuestionId { get; set; }

        public virtual Questions Question { get; set; }
    }
}
