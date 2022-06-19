using Freedom.Core.FrontendVariable;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Freedom.Core.ApiServiceFactory
{
    public class ServiceFactory : IServiceFactory
    {
        // IHttpClientFactory no puedo injectar directamente en el service
        public IHttpClientFactory httpClientFactory { get; private set; }

        public ServiceFactory()
        {
            httpClientFactory = InitializeHttpClient().GetRequiredService<IHttpClientFactory>();


        }

        public ServiceProvider InitializeHttpClient()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient("UrlBase", x => { x.BaseAddress = new Uri(AppGlobalVar.ApiUrlBaseBind.EndPoint); });

            return services.BuildServiceProvider();
        }
    }
}