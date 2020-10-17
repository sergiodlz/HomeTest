using HomeTest.Data;
using HomeTest.Data.Entities;
using HomeTest.Services.Core;
using HomeTest.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HomeTest.Services
{
    public class EntityService<TEntity> : EntityRepository<TEntity>, IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public EntityService(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Create(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DisableAsync(TEntity entity)
        {
            entity.Enable = false;
            Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await FindByCondition(expression).Where(t => t.Enable).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await FindByCondition(t => t.Enable && t.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindByConditionAndIncludeAsync(Expression<Func<TEntity, bool>> expression,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindByConditionAndInclude(expression, includeProperties).Where(t => t.Enable).ToListAsync();
        }

        public async Task<TEntity> GetByIdAndIncludeAsync(Guid id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await FindByConditionAndInclude(t => t.Enable && t.Id.Equals(id), includeProperties).FirstOrDefaultAsync();
        }
    }
}