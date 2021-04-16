using Racing.DB;
using System;
using System.Collections.Generic;

namespace Racing.Repo
{
    public interface IBaseRepo<T> where T : class
    {
        EFContext Database { get; }
        void Add(T entity);
        void Delete(Guid entityId);
        IEnumerable<T> FindAll();
    }
}
