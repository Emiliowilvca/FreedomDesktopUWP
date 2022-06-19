using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Request;

namespace Freedom.Core.ApiServiceImplementation
{
    public class TokenService : ApiService<TokenRequest, TokenRequest>, ITokenService
    {
        public TokenService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}