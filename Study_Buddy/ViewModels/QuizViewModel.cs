using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.DataContexts;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public enum Types
    {
        [Display(Name = "Multiple Choice")]
        MultipleChoice,
        [Display(Name = "Short Answer")]
        FillInTheBlank,
        [Display(Name = "True or False")]
        TrueOrFalse
    }
    public class QuizViewModel
    {
        public int QuizID { get; set; }
        public decimal? Score { get; set; }

        [Display(Name ="What type of questions would you like")]
        public string Type { get; set; }

        public List<SelectListItem> Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "MC", Text = "Multiple Choice" },
            new SelectListItem { Value = "TF", Text = "True or False" },
            new SelectListItem { Value = "SA", Text = "Short Answer"  },
        };

        [Display(Name = "How many items would you like?")]
        public int NumberOfQuestions { get; set; }

        public int numCorrect { get; set; }

        // Logical foreign key to the identity user
        public string Owner { get; set; }

        // Many to one
        [Display(Name ="What topic will this cover?")]
        public int TopicID { get; set; }

        [Display(Name ="Only take from my questions")]
        public bool privateSource { get; set; }

        // Many to many relationship
        public List<Question> Questions { get; set; }

        public Dictionary<int, string> QidGuess { get; set; }

       


    }

}
