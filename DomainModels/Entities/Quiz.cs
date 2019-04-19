using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    public class Quiz
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizID { get; set; }
        public decimal Score  { get; set; }
        public int NumberOfQuestions { get; set; }

        // Logical foreign key to the identity user
        public string Owner { get; set; }

        // Many to one
        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        // Many to many relationship
        public ICollection<QuizQuestionRelation> QuizQuestionRelation { get; set; }
    }
}
