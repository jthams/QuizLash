using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Questions
    {
        public Questions()
        {
            Answers = new HashSet<Answers>();
            Choices = new HashSet<Choices>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int? QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<Choices> Choices { get; set; }
    }
}
