using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace WebUI.ViewModels
{
    public class UserContentViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Quiz> Quizzes { get; set; }
    }
}
