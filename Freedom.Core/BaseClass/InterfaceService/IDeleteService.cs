using Freedom.Utility.Responses;
using System.Threading.Tasks;

namespace Freedom.Core.BaseClass.InterfaceService
{
    public interface IDeleteService
    {
        Task<XResponse> DeleteAsync(int id);
    }
}