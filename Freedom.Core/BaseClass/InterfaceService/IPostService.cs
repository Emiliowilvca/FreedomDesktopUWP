using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.BaseClass.InterfaceService
{
    public interface IPostService<T> where T : class
    {
        Task<ResponsePagination<T>> PostRequest(Pagination pagination, CancellationToken cancellationToken = default);

        Task<XResponse> PostAsync(T entity, CancellationToken cancellationToken = default);
    }
}