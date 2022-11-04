using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Domain
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(object id);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IList<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> match);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> match);
        Task<T> AddAsync(T entity, bool saveChanges = false);
        Task AddRangeAsync(IEnumerable<T> entity, bool saveChanges = false);
        Task UpdateAsync(T entity, bool saveChanges = false);
        Task<T> DeleteAsync(int id, bool saveChanges = false);
        Task<T> DeleteAsync(object id, bool saveChanges = false);
        Task DeleteRangeAsync(IEnumerable<T> entity, bool saveChanges = false);
    }
}
