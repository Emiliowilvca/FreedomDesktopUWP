using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.ApiServiceInterface
{
    public interface IVehicleService : IApiService<VehicleDto, VehicleRTO>
    {
    }
}