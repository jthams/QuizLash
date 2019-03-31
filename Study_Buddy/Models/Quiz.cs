using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Questions>();
        }

        public int QuizId { get; set; }
        public string QuizName { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
    }
}
