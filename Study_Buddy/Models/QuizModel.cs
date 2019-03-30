using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Buddy.Models
{
    public class QuizModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
