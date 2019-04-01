using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Buddy.Models
{
    public enum Type { MultipleChoice, FillInTheBlank, TrueOrFalse }

    public class Question
    {
        public int ID { get; set; }
        public int QuizID { get; set; }
        public Type Type { get; set; }
        public string Body { get; set; }
        public string Answer { get; set; }

    }
}
