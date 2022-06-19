using Freedom.Frontend.Models.BaseEntitySqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Freedom.Core.SQLiteGenericRepository
{
    public interface IGenericRepository<T> : IGenericAsyncRepository<T> where T : IBase, new()
    {
        T Get(Guid id);

        T GetFirstOrDefault();

        List<T> GetAll();

        List<T> GetAll(Expression<Func<T, bool>> predicate = null);

        List<T> GetAll<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);

        bool Exist(Guid Id);

        int Count();

        int Count(Guid id);

        int Count(Expression<Func<T, bool>> predicate);

        int AddOrUpdate(T entity);

        int Insert(T entity);

        int Update(T entity);

        int InsertMultiple(IEnumerable<T> items);

        int Delete(Guid Id);

        CreateTableResult GenerateTable();
    }
}