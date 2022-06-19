using System;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowExist<T>
    {
        Task<bool> ExistByIdAsync(Guid Id);
    }
}