using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> Topics { get; }
    }
}
