using Racing.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Racing.Repo
{
    public class BaseRepo<T> where T : class
    {
        private readonly EFContext dbContext;
        private readonly DbSet<T> dbSet;
       
        public BaseRepo(EFContext efContext)
        {
            dbContext = efContext ?? throw new ArgumentNullException(nameof(efContext));
            dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> FindAll()
        {
            return dbSet.AsEnumerable();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(Guid id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
           
        }
        private void Delete(T entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
    }
}
