using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }
        public string Body { get; set; }
        public string Answer { get; set; }

        // One to many
        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        // Create a logical foreign key to the identity user
        public string Creator { get; set; }

        // Many to many 
        public ICollection<QuizQuestionRelation> QuizQuestionRelation { get; set; }

    }
}
