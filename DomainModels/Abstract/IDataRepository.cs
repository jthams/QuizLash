using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System.Text;

namespace Domain.Abstract
{
    public interface IDataRepository<T>
    {
        IQueryable<T> Items { get; }
        Task<T> FindAsync(int? Id);
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
       
    }
}
