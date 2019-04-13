using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> Questions{ get; }
        
    }
    
}
