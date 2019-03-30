using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int QuestiontypeId { get; set; }
        public int SubTopicId { get; set; }
        public string Body { get; set; }
        public string Answer { get; set; }

        public virtual QuestionType Questiontype { get; set; }
        public virtual Subtopic SubTopic { get; set; }
    }
}
