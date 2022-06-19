using Freedom.Frontend.Models.BaseEntitySqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freedom.Core.SQLiteGenericRepository
{
    public interface IGenericAsyncRepository<T> where T : IBase, new()
    {
        Task<T> GetAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        Task<List<T>> GetAllAsync<TValue>(Expression<Func<T, bool>> predicate = null,
                                  Expression<Func<T, TValue>> orderBy = null);

        Task<bool> ExistAsync(Guid Id);

        Task<int> CountAsync();

        Task<int> CountAsync(Guid id);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<int> AddOrUpdateAsync(T entity);

        Task<int> InsertAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> InsertMultipleAsync(IEnumerable<T> items);

        Task<int> DeleteAsync(Guid Id);

        Task<CreateTableResult> GenerateTableAsync();
    }
}