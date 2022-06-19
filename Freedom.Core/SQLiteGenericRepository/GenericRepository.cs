using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Frontend.Models.BaseEntitySqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Freedom.Core.SQLiteGenericRepository
{
    public class GenericRepository<T> : GenericAsyncRepository<T>, IGenericRepository<T> where T : IBase, new()
    {
        private IDbConnectionFactory _connectionFactory;

        public GenericRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public GenericRepository()
        {
        }

        public T Get(Guid id)
        {
            return _connectionFactory.SQLiteConnection().Get<T>(id);
        }


        public T GetFirstOrDefault()
        {
            return _connectionFactory.SQLiteConnection().Table<T>().FirstOrDefault();
        }
        public List<T> GetAll()
        {
            return _connectionFactory.SQLiteConnection().Table<T>().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var query = _connectionFactory.SQLiteConnection().Table<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.ToList();
        }

        public List<T> GetAll<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _connectionFactory.SQLiteConnection().Table<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query.OrderBy(orderBy);
            }
            return query.ToList();
        }

        public bool Exist(Guid Id)
        {
            return _connectionFactory.SQLiteConnection().Table<T>().Count(x => x.Id == Id) > 0;
        }

        public int Count()
        {
            return _connectionFactory.SQLiteConnection().Table<T>().Count();
        }

        public int Count(Guid id)
        {
            return _connectionFactory.SQLiteConnection().Table<T>().Count(x => x.Id == id);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _connectionFactory.SQLiteConnection().Table<T>().Count(predicate);
        }

        public int Insert(T entity)
        {
            return _connectionFactory.SQLiteConnection().Insert(entity);
        }

        public int Update(T entity)
        {
            return _connectionFactory.SQLiteConnection().Update(entity);
        }

        public int InsertMultiple(IEnumerable<T> items)
        {
            return _connectionFactory.SQLiteConnection().InsertAll(items);
        }

        public int Delete(Guid Id)
        {
            return _connectionFactory.SQLiteConnection().Delete(Id);
        }

        public CreateTableResult GenerateTable()
        {
            return _connectionFactory.SQLiteConnection().CreateTable<T>();
        }

        public int AddOrUpdate(T entity)
        {
            bool exist = _connectionFactory.SQLiteConnection()
                                         .Table<T>()
                                         .Count(x => x.Id == entity.Id) > 0;
            if (!exist)
            {
                return _connectionFactory.SQLiteConnection().Insert(entity);
            }
            else
            {
                return _connectionFactory.SQLiteConnection().Update(entity);
            }
        }

        
    }
}