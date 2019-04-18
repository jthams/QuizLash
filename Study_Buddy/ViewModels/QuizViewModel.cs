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

    public class QuizViewModel
    {
        public int QuizID { get; set; }
        public decimal? Score { get; set; }

        [Required]
        public string Owner { get; set; }
        
        [Display(Name ="What type of questions would you like")]
        public string Type { get; set; }

        public List<SelectListItem> Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "MC", Text = "Multiple Choice" },
            new SelectListItem { Value = "TF", Text = "True or False" },
            new SelectListItem { Value = "SA", Text = "Short Answer"  }
        };

        [Required]
        [Display(Name = "What topic will this cover?")]
        public int TopicID { get; set; }

        [Required]
        [Display(Name = "How many items would you like?")]
        public int NumberOfQuestions { get; set; }

        [Required]
        [Display(Name ="Only take from my questions")]
        public bool PrivateSource { get; set; }

        // Collection of questions to display to the user
        public List<Question> Questions { get; set; }

        // Dictionary for grading SA quizzes
        public Dictionary<int, string> QidGuess { get; set; }

    }

}
