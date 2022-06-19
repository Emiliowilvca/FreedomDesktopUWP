using Freedom.Utility.Responses;
using System.Threading.Tasks;

namespace Freedom.Core.BaseClass.InterfaceService
{
    public interface IPutService<T> where T : class
    {
        Task<XResponse> PutAsync(T entity);
    }
}