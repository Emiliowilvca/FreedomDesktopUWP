using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.ApiServiceImplementation
{
    public class UserSettingService : ApiService<UserSettingDto, UserSettingRTO>, IUserSettingService
    {
        public UserSettingService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}