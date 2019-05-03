using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebUI.ViewModels
{
    public class UserAccountViewModel
    {
          
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        public string LoginProvider { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<IdentityError> Errors { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Quiz> Quizzes { get; set; }
        public Dictionary<int, decimal> TopicAverages { get; set; }
    }
}
