using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.ApiServiceImplementation
{
    public class MeaseureService : ApiService<MeasureDto, MeasureRTO>, IMeasureService
    {
        public MeaseureService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}