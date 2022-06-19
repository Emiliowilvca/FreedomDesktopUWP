using System.Net.Http;

namespace Freedom.Core.ApiServiceFactory
{
    public interface IServiceFactory
    {
        IHttpClientFactory httpClientFactory { get; }
    }
}