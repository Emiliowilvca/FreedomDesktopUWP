using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowGetAll<T>
    {
        Task<List<T>> GetAllAsync();
    }
}