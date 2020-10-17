using HomeTest.Data;
using HomeTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HomeTest.Services.Repository
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected ApplicationDbContext RepositoryContext { get; set; }

        public EntityRepository(ApplicationDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void Create(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> FindAll()
        {
            return RepositoryContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public IQueryable<TEntity> FindByConditionAndInclude(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public void Update(TEntity entity)
        {
            RepositoryContext.Set<TEntity>().Update(entity);
        }
    }
}