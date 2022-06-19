using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.ApiServiceImplementation
{
    public class OperationSubClassService : ApiService<SubClassOperationRTO, SubClassOperationRTO>, IOperationSubClassService
    {
        public OperationSubClassService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}