using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Interfaces;

namespace LibraryManagement.Core.Services
{
    public class JsonRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> _entities;
        private readonly Action<IEnumerable<T>> _saveAction;
        private int _nextId = 1;

        public JsonRepository(IEnumerable<T> initialData, Action<IEnumerable<T>> saveAction)
        {
            _entities = new List<T>(initialData ?? Enumerable.Empty<T>());
            _saveAction = saveAction;

            if (_entities.Any())
            {
                _nextId = _entities.Max(e => e.Id) + 1;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public T? GetById(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Id = _nextId++;
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var index = _entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _entities[index] = entity;
            }
        }

        public void Delete(int id)
        {
            _entities.RemoveAll(e => e.Id == id);
        }

        public void SaveChanges()
        {
            _saveAction(_entities);
        }
    }
} 