using Challenge.Infrastructure.EfCore;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.EFCore.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly ChallengeContext _dbContext;
        
        public RepositoryBase(ChallengeContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity, bool saveChanges = false)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
            return entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entity, bool saveChanges = false)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(T entity, bool saveChanges = false)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                if (saveChanges)
                {
                    var res = await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
           
        }

        public async Task<T> DeleteAsync(int id, bool saveChanges = false)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _dbContext.Set<T>().Remove(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = _dbContext.Set<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;
        }

        public async Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext
                 .Set<T>().Where(match).ToListAsync();
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext
                 .Set<T>().AsNoTracking().Where(match).FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().AnyAsync(expression);
        }
        public async Task<int> CountAsync()
        {
            return await _dbContext
                 .Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext
                 .Set<T>().Where(match).CountAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity, bool saveChanges = false)
        {
            _dbContext.Set<T>().RemoveRange(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<T> DeleteAsync(object id, bool saveChanges = false)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _dbContext.Set<T>().Remove(entity);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }

            return entity;
        }
    }
}
