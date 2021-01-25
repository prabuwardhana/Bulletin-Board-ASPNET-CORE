using System;
using System.Linq;
using System.Linq.Expressions;
using Entities;
using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _repositoryContext;

        public RepositoryBase(RepositoryContext repositorycontext)
        {
            _repositoryContext = repositorycontext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _repositoryContext.Set<T>().AsNoTracking() :
            _repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            _repositoryContext.Set<T>().Where(expression).AsNoTracking() :
            _repositoryContext.Set<T>().Where(expression);

        public void Create(T entity) => _repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity) => _repositoryContext.Set<T>().Remove(entity);

        public void CascadeDelete(ICollection<T> entity) => _repositoryContext.Set<T>().RemoveRange(entity);

        public void Update(T entity) => _repositoryContext.Set<T>().Update(entity);
    }
}
