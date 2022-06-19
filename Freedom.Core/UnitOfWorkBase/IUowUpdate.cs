using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowUpdate<T>
    {
        Task UpdateAsync(T entity);
    }
}