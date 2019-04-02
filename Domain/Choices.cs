using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study_Buddy.Models
{
    public class Choices
    {
        public int ID { get; set; }

        [ForeignKey("Question")]
        public int QuestionRefID { get; set; }
        public Question Question { get; set; }

        public string Text { get; set; }
    }
}
