using System;
using System.Collections.Generic;

namespace Study_Buddy.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Quiz = new HashSet<Quiz>();
            Subtopic = new HashSet<Subtopic>();
        }

        public int TopicId { get; set; }
        public string TopicDesc { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
        public virtual ICollection<Subtopic> Subtopic { get; set; }
    }
}
