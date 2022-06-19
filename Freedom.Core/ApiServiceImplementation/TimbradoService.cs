using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Frontend.Models.AllPurpose;

namespace Freedom.Core.ApiServiceImplementation
{
    public class TimbradoService : ApiService<Timbrado, Timbrado>, ITimbradoService
    {
        public TimbradoService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}