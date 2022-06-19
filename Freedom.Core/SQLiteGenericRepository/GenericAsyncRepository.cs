using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Frontend.Models.BaseEntitySqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Freedom.Core.SQLiteGenericRepository
{
    public class GenericAsyncRepository<T> : IGenericAsyncRepository<T> where T : IBase, new()
    {
        private IDbConnectionFactory _connectionFactory;

        public GenericAsyncRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public GenericAsyncRepository()
        {
        }

        public async Task<CreateTableResult> GenerateTableAsync()
        {
            return await _connectionFactory.SQLiteAsyncConnection().CreateTableAsync<T>();
        }

        public async Task<T> GetAsync(Guid id)
        {
            T item = await _connectionFactory.SQLiteAsyncConnection()
                                             .Table<T>()
                                             .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> items = await _connectionFactory.SQLiteAsyncConnection()
                                                    .Table<T>()
                                                    .ToListAsync();
            return items;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _connectionFactory.SQLiteAsyncConnection().Table<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TValue>(Expression<Func<T, bool>> predicate = null,
          Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _connectionFactory.SQLiteAsyncConnection().Table<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query.OrderBy(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _connectionFactory.SQLiteAsyncConnection()
                                           .Table<T>()
                                           .CountAsync();
        }

        public async Task<int> CountAsync(Guid id)
        {
            return await _connectionFactory.SQLiteAsyncConnection()
                                           .Table<T>()
                                           .Where(x => x.Id == id)
                                           .CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            var query = _connectionFactory.SQLiteAsyncConnection().Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.CountAsync();
        }

        public async Task<bool> ExistAsync(Guid Id)
        {
            return await _connectionFactory.SQLiteAsyncConnection()
                                            .Table<T>()
                                            .CountAsync(x => x.Id == Id) > 0;
        }

        public async Task<int> InsertAsync(T item)
        {
            return await _connectionFactory.SQLiteAsyncConnection().InsertAsync(item);
        }

        public async Task<int> InsertMultipleAsync(IEnumerable<T> items)
        {
            return await _connectionFactory.SQLiteAsyncConnection().InsertAllAsync(items);
        }

        public async Task<int> UpdateAsync(T item)
        {
            return await _connectionFactory.SQLiteAsyncConnection().UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(Guid Id)
        {
            return await _connectionFactory.SQLiteAsyncConnection().DeleteAsync(Id);
        }

        public async Task<int> AddOrUpdateAsync(T entity)
        {
            bool exist = await _connectionFactory.SQLiteAsyncConnection()
                                          .Table<T>()
                                          .CountAsync(x => x.Id == entity.Id) > 0;
            if (!exist)
            {
                return await _connectionFactory.SQLiteAsyncConnection().InsertAsync(entity);
            }
            else
            {
                return await _connectionFactory.SQLiteAsyncConnection().UpdateAsync(entity);
            }
        }
    }
}