using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkingWithIdentity.Contracts;
using WorkingWithIdentity.Data;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.MainRepository
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        public DbSet<T> dbSet;
        public ApplicationDbContext context;
        public SqlRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public bool Exist(string id)
        {
            return this.dbSet.Any(e => e.Id.Equals(id));
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        //public IQueryable<T> Where(Expression<Func<T,bool>>predicate)
        //{
        //    return dbSet.Where(predicate);
        //}
        
    }
}
