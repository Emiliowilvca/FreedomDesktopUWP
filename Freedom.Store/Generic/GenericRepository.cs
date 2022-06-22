using Freedom.Factory.Generic;
using Freedom.Frontend.Models.BaseEntitySqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace Freedom.Store.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBase
    {
        private readonly string _fullPath;
        private readonly IPathFactory _pathFactory;
        private readonly string _fileName;
        private readonly JsonSerializerOptions _options;

        public GenericRepository(IPathFactory pathFactory)
        {
            _pathFactory = pathFactory;
            _fileName = $"{typeof(T).Name}.json";
            _fullPath = Path.Combine(_pathFactory.GetBasePath(), _fileName);
            _options = new JsonSerializerOptions { WriteIndented = false, PropertyNameCaseInsensitive = false };
            CheckIfDirectoryExist(_pathFactory.GetBasePath());
            CreateDefaultFile(_fullPath);
        }

        public async Task<bool> ExistAsync(Guid Id)
        {
            try
            {
                await Task.Delay(0);
                string readString = File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                return collection.Any(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                await Task.Delay(0);
                string readString = File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                return collection.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                await Task.Delay(0);
                string readString =   File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                return collection.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                await Task.Delay(0);
                string readString =   File.ReadAllText(_fullPath);
                IQueryable<T> collection = JsonSerializer.Deserialize<List<T>>(readString).AsQueryable();
                return collection.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task InsertAsync(T entity)
        {
            try
            {
                await Task.Delay(0);
                string readString = File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                collection.Add(entity);
                string jsonString = JsonSerializer.Serialize(collection, _options);
                  File.WriteAllText(_fullPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            try
            {
                await Task.Delay(0);
                string readString =   File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);

                T ent = collection.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (ent != null)
                {
                    int index = collection.IndexOf(ent);
                    if (index == -1)
                        throw new Exception("entity is null");
                    collection[index] = entity;
                }
                else
                {
                    collection.Add(entity);
                }
                string jsonString = JsonSerializer.Serialize(collection, _options);
                  File.WriteAllText(_fullPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                await Task.Delay(0);
                string readString =   File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                T ent = collection.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (ent == null)
                    throw new Exception("entity is null");
                int index = collection.IndexOf(ent);
                if (index == -1)
                    throw new Exception("Index Collection is -1");
                collection[index] = entity;
                string jsonString = JsonSerializer.Serialize(collection, _options);
                  File.WriteAllText(_fullPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid Id)
        {
            try
            {
                await Task.Delay(0);
                string readString =   File.ReadAllText(_fullPath);
                List<T> collection = JsonSerializer.Deserialize<List<T>>(readString);
                T ent = collection.Where(x => x.Id == Id).FirstOrDefault();
                if (ent == null)
                    throw new Exception("entity is null");
                collection.Remove(ent);
                string jsonString = JsonSerializer.Serialize(collection, _options);
                  File.WriteAllText(_fullPath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CheckIfDirectoryExist(string basePath)
        {
            if (!Directory.Exists(basePath))
            {
              //  DirectoryInfo di = Directory.CreateDirectory(basePath);
                // di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }

        private void CreateDefaultFile(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                List<T> collection = new List<T>();
                string jsonString = JsonSerializer.Serialize(collection, _options);
                File.WriteAllText(_fullPath, jsonString);
            }
        }
    }
}