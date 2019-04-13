using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Choice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChoiceID { get; set; }
        public string Text { get; set; }

        // Many to one
        public int QuestionID { get; set; }
        public Question Question { get; set; }
    }
}
