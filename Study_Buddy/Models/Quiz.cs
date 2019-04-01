using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Buddy.Models
{
    
    public class Quiz
    {
        public int ID { get; set; }
        public string OwnerID { get; set; }
        public string Topic { get; set; }
        
        public ICollection<Question> Questions { get; set; }

        public decimal? Score { get; set; }


    }
}
