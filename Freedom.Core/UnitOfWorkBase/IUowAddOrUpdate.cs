using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowAddOrUpdate<T>
    {
        Task AddOrUpdateAsync(T entity);
    }
}