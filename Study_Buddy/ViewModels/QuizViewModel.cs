using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class QuizViewModel
    {
        public int QuizID { get; set; }
        public decimal? Score { get; set; }

        [Display(Name ="What type of questions would you like")]
        public Type type { get; set; }

        [Display(Name = "How many questions would you like?")]
        public int NumberOfQuestions { get; set; }

        // Logical foreign key to the identity user
        public string Owner { get; set; }

        // Many to one
        [Display(Name ="What topic will this cover?")]
        public int TopicID { get; set; }

        // Many to many relationship
        public IEnumerable<Question> Questions { get; set; }

        public enum Type
        {
            [Display(Name = "Multiple Choice")]
            MultipleChoice,
            [Display(Name = "Short Answer")]
            FillInTheBlank

            // Not yet created
            //[Display(Name = "True or False")]
            //TrueOrFalse
        }
    }
}
