using System;
using System.Threading.Tasks;

namespace Freedom.Core.UnitOfWorkBase
{
    public interface IUowGetById<T>
    {
        Task<T> GetByIdAsync(Guid Id);
    }
}