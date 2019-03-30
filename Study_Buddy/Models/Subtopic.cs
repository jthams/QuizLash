using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Subtopic
    {
        public Subtopic()
        {
            Question = new HashSet<Question>();
        }

        public int SubtopicId { get; set; }
        public string SubtopicDesc { get; set; }
        public int? TopicId { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
