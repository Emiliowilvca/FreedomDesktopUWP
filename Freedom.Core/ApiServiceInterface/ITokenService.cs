using Freedom.Core.ApiServices;
using Freedom.Utility.Request;

namespace Freedom.Core.ApiServiceInterface
{
    public interface ITokenService : IApiService<TokenRequest, TokenRequest>
    {
    }
}