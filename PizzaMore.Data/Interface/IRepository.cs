using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PizzaMore.Data.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity GetById(int id);
        TEntity First(Expression<Func<TEntity, bool>> expression);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        int Count();
        int Count(Expression<Func<TEntity, bool>> expression);
    }
}
