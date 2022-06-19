using Freedom.Core.ApiServices;
using Freedom.Utility.Request;

namespace Freedom.Core.ApiServiceInterface
{
    public interface IUserConfirmService : IApiService<ConfirmEmailRequest, ConfirmEmailRequest>
    {
    }
}