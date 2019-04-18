using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionID { get; set; }
        
        [Required]
        public string Creator { get; set; }

        [Required]
        [Display(Name ="Question")]
        public string Body { get; set; }

        [Required]
        [Display(Name ="Answer")]
        public string Answer { get; set; }
        
        [Required]
        [Display(Name ="Topic")]
        public int TopicID { get; set; }

        [Display(Name ="Option 2")]
        public string Choice1 { get; set; }
        [Display(Name = "Option 3")]
        public string Choice2 { get; set; }
        [Display(Name = "Option 4")]
        public string Choice3 { get; set; }
        public ICollection<QuizQuestionRelation> QuizQuestionRelation { get; set; }
    }
}
