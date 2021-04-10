using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected PurchaseDbContext _purchaseContext;

        public RepositoryBase(PurchaseDbContext purchaseContext)
        {
            _purchaseContext = purchaseContext;
        }

        public void Create(T entity)
        {
            _purchaseContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _purchaseContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _purchaseContext.Set<T>().AsNoTracking()
            : _purchaseContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _purchaseContext.Set<T>().Where(expression).AsNoTracking()
            : _purchaseContext.Set<T>().Where(expression);

        public IQueryable<T> FindWithInclude(Expression<Func<T, bool>> expression,
            bool trackChanges,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var context = _purchaseContext.Set<T>().Where(expression);
            if (!trackChanges)
            {
                context = context.AsNoTracking();
            } 

            return includeProperties.Aggregate(context, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Update(T entity)
        {
            _purchaseContext.Set<T>().Update(entity);
        }
    }
}
