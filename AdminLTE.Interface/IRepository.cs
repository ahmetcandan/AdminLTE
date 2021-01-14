using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdminLTE.Interface
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(params object[] keyValues);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
    }
}
