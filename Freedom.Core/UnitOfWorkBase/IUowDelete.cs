using System;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowDelete<T>
    {
        Task DeleteAsync(Guid Id);
    }
}