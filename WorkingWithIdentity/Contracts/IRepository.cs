using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        T Find(string Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exist(string id);

    }
}
