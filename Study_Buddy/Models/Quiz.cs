using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Quiz
    {
        public int QuizId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int TopicId { get; set; }
        public int NumberOfQuestions { get; set; }
        public decimal? Score { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
