using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PizzaMore.Data.Interface;

namespace PizzaMore.Data
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> set;

        public Repository(DbSet<TEntity> set)
        {
            this.set = set;
        }
        public void Add(TEntity entity)
        {
            set.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            set.AddRange(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            set.RemoveRange(entities);
        }

        public TEntity GetById(int id)
        {
            return set.Find(id);
        }

        public TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return set.First(expression);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return set.FirstOrDefault(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.Where(f=>true);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return set.Where(expression);
        }

        public int Count()
        {
            return set.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            return set.Count(expression);
        }
    }
}
