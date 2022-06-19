using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.ApiServiceImplementation
{
    public class ProviderService : ApiService<ProviderDto, ProviderRTO>, IProviderService
    {
        private readonly IServiceFactory _serviceFactory;

        public ProviderService(IServiceFactory serviceFactory) : base(serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<EntityResponse<ProviderDtoFull>> GetProviderDtoFull(string servicePrefix, string version, string controller, string methods, int id, CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpClient httpClient = _serviceFactory.httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response,
                                     new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    EntityResponse<ProviderDtoFull> entityResponse = new EntityResponse<ProviderDtoFull>();
                    entityResponse.IsSuccess = false;
                    entityResponse.Entity = default;
                    entityResponse.Message = "";
                    entityResponse.StatusCode = httpResponse.StatusCode;
                    if (errorResponse is null)
                    {
                        entityResponse.Message = "ErrorResponse is Null (undefined error)";
                    }
                    ErrorModel errorItem = errorResponse.ErrorCollection.FirstOrDefault();
                    if (errorItem is null)
                    {
                        entityResponse.Message = "ErrorModel is Null (undefined error)";
                    }
                    string message = _localizeResource.GetDescription(errorItem.ResxCode.NullIfEmpty() ?? errorItem.Menssage);
                    entityResponse.Message = message;
                }

                ProviderDtoFull entity = JsonSerializer.Deserialize<ProviderDtoFull>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return new EntityResponse<ProviderDtoFull>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<ProviderDtoFull>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<ProviderDtoFull>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }
    }
}