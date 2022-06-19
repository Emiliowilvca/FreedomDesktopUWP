using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowInsert<T>
    {
        Task InsertAsync(T entity);
    }
}