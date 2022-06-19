using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Request;

namespace Freedom.Core.ApiServiceImplementation
{
    public class UserConfirmService : ApiService<ConfirmEmailRequest, ConfirmEmailRequest>, IUserConfirmService
    {
        public UserConfirmService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}