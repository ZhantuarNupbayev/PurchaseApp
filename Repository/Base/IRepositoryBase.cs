using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        IQueryable<T> FindWithInclude(Expression<Func<T, bool>> expression, bool trackChanges, params Expression<Func<T, object>>[] includeProperties);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
