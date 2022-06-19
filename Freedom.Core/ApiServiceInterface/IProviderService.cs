using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.ApiServiceInterface
{
    public interface IProviderService : IApiService<ProviderDto, ProviderRTO>
    {
        Task<EntityResponse<ProviderDtoFull>> GetProviderDtoFull(string servicePrefix, string version, string controller, string methods, int id,
                                              CancellationToken cancellationToken = default, string token = "");
    }
}