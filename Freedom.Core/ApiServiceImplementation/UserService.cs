using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.ApiServiceImplementation
{
    public class UserService : ApiService<UserDto, UserRTO>, IUserService
    {
        public UserService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}