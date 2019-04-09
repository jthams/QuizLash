using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Topic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicID { get; set; }
        public string Description { get; set; }

        // One to many
        public ICollection<Question> Questions { get; set; }

        // One to many
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
