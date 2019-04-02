using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study_Buddy.Models
{
    public enum Type { MultipleChoice, FillInTheBlank, TrueOrFalse }

    public class Question
    {
        public int QuestionID { get; set; }

        public int QuizRefID { get; set; }
        public Quiz Quiz { get; set; }

        public Type Type { get; set; }
        public string Body { get; set; }
        public string Answer { get; set; }

        public ICollection<Choices> Choices { get; set; }
       

    }
}
