using Freedom.Core.ApiServiceFactory;
using Freedom.Core.CoreInterface;
using Freedom.Core.Implement;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.ApiServices
{
    public class ApiService<TD, TR> : IApiService<TD, TR> where TD : class where TR : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public readonly ILocalizeResource _localizeResource;
        private readonly string _urlBase = "UrlBase";
        private JsonSerializerOptions _jsonOptions;

        public ApiService(IServiceFactory serviceFactory)
        {
            _httpClientFactory = serviceFactory.httpClientFactory;
            _localizeResource = new LocalizeResource();
            _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<EntityResponse<T>> GetEntityFullAsync<T>(string servicePrefix, string version, string controller, string methods, int id,
                                                              CancellationToken cancellationToken = default, string token = "") where T : class
        {
            try
            {
                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<T>(response, httpResponse.StatusCode);
                }

                T entity = JsonSerializer.Deserialize<T>(response, _jsonOptions);

                return new EntityResponse<T>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<T>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<EntityResponse<T>> GetEntityFullAsync<T, P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                                             CancellationToken cancellationToken = default, string token = "") where T : class where P : class
        {
            try
            {
                string urlParameter = parameter.BuildQueryString();

                string url = $"{servicePrefix}{version}{controller}{methods}?{urlParameter}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<T>(response, httpResponse.StatusCode);
                }

                T entity = JsonSerializer.Deserialize<T>(response, _jsonOptions);

                return new EntityResponse<T>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<T>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<T>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<EntityResponse<TD>> GetEntityAsync<P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                                            CancellationToken cancellationToken = default, string token = "") where P : class
        {
            try
            {
                string urlParameter = parameter.BuildQueryString();

                string url = $"{servicePrefix}{version}{controller}{methods}?{urlParameter}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<TD>(response, httpResponse.StatusCode);
                }

                TD entity = JsonSerializer.Deserialize<TD>(response, _jsonOptions);

                return new EntityResponse<TD>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<TD>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<TD>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<EntityResponse<TD>> GetEntityAsync(string servicePrefix, string version, string controller, string methods, int id,
                                                             CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<TD>(response, httpResponse.StatusCode);
                }

                TD entity = JsonSerializer.Deserialize<TD>(response, _jsonOptions);

                return new EntityResponse<TD>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<TD>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<TD>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<EntityResponse<TR>> GetEntityRtoAsync<P>(string servicePrefix, string version, string controller, string methods,
                                                            P parameter, CancellationToken cancellationToken = default, string token = "") where P : class
        {
            try
            {
                string urlParameter = parameter.BuildQueryString();

                string url = $"{servicePrefix}{version}{controller}{methods}?{urlParameter}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<TR>(response, httpResponse.StatusCode);
                }

                TR entity = JsonSerializer.Deserialize<TR>(response, _jsonOptions);

                return new EntityResponse<TR>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<TR>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<TR>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<EntityResponse<TR>> GetEntityRtoAsync(string servicePrefix, string version, string controller, string methods,
                                                                int id, CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateEntityResponse<TR>(response, httpResponse.StatusCode);
                }

                TR entity = JsonSerializer.Deserialize<TR>(response, _jsonOptions);

                return new EntityResponse<TR>() { Entity = entity, IsSuccess = true, Message = "", StatusCode = httpResponse.StatusCode };
            }
            catch (TaskCanceledException)
            {
                return new EntityResponse<TR>
                {
                    IsSuccess = false,
                    Message = Lang.CancelByTocken,
                    Entity = default
                };
            }
            catch (Exception ex)
            {
                return new EntityResponse<TR>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Entity = default
                };
            }
        }

        public async Task<ListResponse<TR>> GetListAsync<P>(string servicePrefix, string version, string controller, string methods,
                                                                    P parameter, CancellationToken cancellationToken = default, string token = "") where P : class
        {
            try
            {
                string urlParameter = parameter.BuildQueryString();

                string url = $"{servicePrefix}{version}{controller}{methods}?{urlParameter}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //await Task.Delay(10000);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateListResponse(response, httpResponse.StatusCode);
                }

                List<TR> collection = JsonSerializer.Deserialize<List<TR>>(response, _jsonOptions);

                return new ListResponse<TR>() { IsSuccess = true, Message = "", Collection = collection };
            }
            catch (TaskCanceledException)
            {
                return new ListResponse<TR>() { Message = Lang.CancelByTocken, IsSuccess = false };
            }
            catch (Exception ex)
            {
                return new ListResponse<TR>() { Message = ex.Message, IsSuccess = false };
            }
        }

        public async Task<ListResponse<TR>> GetListAsync(string servicePrefix, string version, string controller, string methods, string parameter,
                                                    CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string url = $"{servicePrefix}{version}{controller}{methods}{parameter}";

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.GetAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateListResponse(response, httpResponse.StatusCode);
                }

                List<TR> collection = JsonSerializer.Deserialize<List<TR>>(response, _jsonOptions);

                return new ListResponse<TR>() { IsSuccess = true, Message = "", Collection = collection };
            }
            catch (TaskCanceledException)
            {
                return new ListResponse<TR>() { Message = Lang.CancelByTocken, IsSuccess = false };
            }
            catch (Exception ex)
            {
                return new ListResponse<TR>() { Message = ex.Message, IsSuccess = false };
            }
        }

        public async Task<TokenResponse> GetTokenAsync(string servicePrefix, string version, string controller, string methods, TokenRequest tokenRequest,
                                                       CancellationToken cancellationToken = default)
        {
            try
            {
                string request = JsonSerializer.Serialize(tokenRequest);

                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                string url = $"{servicePrefix}{version}{controller}{methods}";

                HttpResponseMessage httpResponse = await httpClient.PostAsync(url, content, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateTokenResponse(response);
                }
                return JsonSerializer.Deserialize<TokenResponse>(response, _jsonOptions);
            }
            catch (TaskCanceledException)
            {
                return new TokenResponse { Success = false, ResultMessage = Lang.CancelByTocken };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new TokenResponse() { ResultMessage = ex.Message, Success = false };
            }
        }

        public async Task<XResponse> PostAsync(string servicePrefix, string version, string controller, string methods, TD model,
                                               CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string request = JsonSerializer.Serialize(model);

                string url = $"{servicePrefix}{version}{controller}{methods}";

                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClient httpClient = _httpClientFactory.CreateClient(_urlBase);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage httpResponse = await httpClient.PostAsync(url, content, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateErrorResponse(response);
                }
                XResponse obj = JsonSerializer.Deserialize<XResponse>(response, _jsonOptions);
                return obj;
            }
            catch (TaskCanceledException)
            {
                return new XResponse() { ResultMessage = Lang.CancelByTocken, Success = false };
            }
            catch (Exception ex)
            {
                return new XResponse() { ResultMessage = ex.Message, Success = false };
            }
        }

        public async Task<XResponse> PutAsync(string servicePrefix, string version, string controller, string methods, TD model,
                                              CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                string request = JsonSerializer.Serialize(model);

                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string url = $"{servicePrefix}{version}{controller}{methods}";

                HttpResponseMessage httpResponse = await httpClient.PutAsync(url, content, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateErrorResponse(response);
                }
                XResponse obj = JsonSerializer.Deserialize<XResponse>(response, _jsonOptions);
                return obj;
            }
            catch (TaskCanceledException)
            {
                return new XResponse() { ResultMessage = Lang.CancelByTocken, Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new XResponse() { ResultMessage = ex.Message, Success = false };
            }
        }

        public async Task<XResponse> DeleteAsync(string servicePrefix, string version, string controller, string methods, int id,
                                          CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpResponseMessage httpResponse = await httpClient.DeleteAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateErrorResponse(response);
                }

                XResponse obj = JsonSerializer.Deserialize<XResponse>(response, _jsonOptions);

                return obj;
            }
            catch (TaskCanceledException)
            {
                return new XResponse() { ResultMessage = Lang.CancelByTocken, Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new XResponse() { ResultMessage = ex.Message, Success = false };
            }
        }

        public async Task<XResponse> DeleteAsync(string servicePrefix, string version, string controller, string methods, Guid id, CancellationToken cancellationToken = default, string token = "")
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("UrlBase");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string url = $"{servicePrefix}{version}{controller}{methods}/{id}";

                HttpResponseMessage httpResponse = await httpClient.DeleteAsync(url, cancellationToken);

                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return CreateErrorResponse(response);
                }
                XResponse obj = JsonSerializer.Deserialize<XResponse>(response, _jsonOptions);
                return obj;
            }
            catch (TaskCanceledException)
            {
                return new XResponse() { ResultMessage = Lang.CancelByTocken, Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new XResponse() { ResultMessage = ex.Message, Success = false };
            }
        }

        private ListResponse<TR> CreateListResponse(string response, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response,
                                     _jsonOptions);

            ListResponse<TR> listResponse = new ListResponse<TR>();
            listResponse.IsSuccess = false;
            listResponse.Collection = new List<TR>();
            listResponse.Message = "";
            listResponse.StatusCode = statusCode;
            if (errorResponse is null)
            {
                listResponse.Message = "ErrorResponse is Null (undefined error)";
            }
            ErrorModel errorItem = errorResponse.ErrorCollection.FirstOrDefault();
            if (errorItem is null)
            {
                listResponse.Message = "ErrorModel is Null (undefined error)";
            }
            string message = _localizeResource.GetDescription(errorItem.ResxCode.NullIfEmpty() ?? errorItem.Menssage);
            listResponse.Message = message;
            return listResponse;
        }

        private EntityResponse<TEntity> CreateEntityResponse<TEntity>(string response, HttpStatusCode statusCode) where TEntity : class
        {
            ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response,
                                            _jsonOptions);

            EntityResponse<TEntity> entityResponse = new EntityResponse<TEntity>();
            entityResponse.IsSuccess = false;
            entityResponse.Entity = default;
            entityResponse.Message = "";
            entityResponse.StatusCode = statusCode;
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
            return entityResponse;
        }

        private TokenResponse CreateTokenResponse(string response)
        {
            ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response, _jsonOptions);
            if (errorResponse is null)
            {
                return new TokenResponse { Success = false, ResultMessage = "ErrorResponse is Null (undefined error)" };
            }
            ErrorModel errorItem = errorResponse.ErrorCollection.FirstOrDefault();
            if (errorItem is null)
            {
                return new TokenResponse { Success = false, ResultMessage = "ErrorModel is Null (undefined error)" };
            }
            string message = _localizeResource.GetDescription(errorItem.ResxCode.NullIfEmpty() ?? errorItem.Menssage);
            return new TokenResponse { Success = false, ResultMessage = message, ResourceKey = errorItem.ResxCode };
        }

        private XResponse CreateErrorResponse(string response)
        {
            ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response, _jsonOptions);
            XResponse xResponse = new XResponse();
            xResponse.ResultMessage = "";
            xResponse.Success = false;
            if (errorResponse is null)
            {
                xResponse.ResultMessage = "ErrorResponse is Null (undefined error)";
            }
            ErrorModel errorItem = errorResponse.ErrorCollection.FirstOrDefault();
            if (errorItem is null)
            {
                xResponse.ResultMessage = "ErrorModel is Null (undefined error)";
            }
            string message = _localizeResource.GetDescription(errorItem.Menssage.NullIfEmpty() ?? errorItem.ResxCode);
            xResponse.ResultMessage = message;
            return xResponse;
        }
    }
}