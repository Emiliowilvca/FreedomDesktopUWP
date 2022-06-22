using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freedom.Store.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> ExistAsync(Guid Id);

        Task<T> GetAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(Guid Id);

        Task AddOrUpdateAsync(T entity);
    }
}