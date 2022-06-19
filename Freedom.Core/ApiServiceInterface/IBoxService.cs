using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.ApiServiceInterface
{
    public interface IBoxService : IApiService<BoxDto, BoxRTO>
    {
        Task<EntityResponse<BoxDtoFull>> GetBoxDtoFull(string servicePrefix, string version, string controller, string methods, int id,
                                                CancellationToken cancellationToken = default, string token = "");
    }
}