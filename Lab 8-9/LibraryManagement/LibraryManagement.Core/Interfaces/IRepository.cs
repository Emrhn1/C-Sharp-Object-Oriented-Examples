using System.Collections.Generic;

namespace LibraryManagement.Core.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();
    }
} 