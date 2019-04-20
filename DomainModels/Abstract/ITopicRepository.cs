using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Text;

namespace Domain.Abstract
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> Topics { get; }
    }
}
