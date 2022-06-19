using Freedom.Utility.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freedom.Core.BaseClass.InterfaceService
{
    public interface IGetService<T>
    {
        Task<List<T>> GetAsync(Pagination pagination);
    }
}