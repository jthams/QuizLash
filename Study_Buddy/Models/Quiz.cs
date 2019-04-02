using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study_Buddy.Models
{

    public class Quiz
    { 
        public int QuizID { get; set; }
        public string Owner { get; set; }
        public string Topic { get; set; }

        [ForeignKey("QuizRefID")]
        public ICollection<Question> Questions { get; set; }
        public bool IsComplete { get; set; }
        public decimal? Score { get; set; }
    }
}
