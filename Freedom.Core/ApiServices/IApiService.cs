using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Freedom.Core.ApiServices
{
    public interface IApiService<TD, TR> where TD : class where TR : class
    {
        Task<EntityResponse<T>> GetEntityFullAsync<T>(string servicePrefix, string version, string controller, string methods, int id,
                                                              CancellationToken cancellationToken = default, string token = "") where T : class;

        Task<EntityResponse<T>> GetEntityFullAsync<T, P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                                             CancellationToken cancellationToken = default, string token = "") where T : class where P : class;

        Task<EntityResponse<TD>> GetEntityAsync(string servicePrefix, string version, string controller, string methods, int id,
                                                CancellationToken cancellationToken = default, string token = "");

        Task<EntityResponse<TD>> GetEntityAsync<P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                                CancellationToken cancellationToken = default, string token = "") where P : class;

        Task<EntityResponse<TR>> GetEntityRtoAsync(string servicePrefix, string version, string controller, string methods, int id,
                                               CancellationToken cancellationToken = default, string token = "");

        Task<EntityResponse<TR>> GetEntityRtoAsync<P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                              CancellationToken cancellationToken = default, string token = "") where P : class;

        Task<ListResponse<TR>> GetListAsync<P>(string servicePrefix, string version, string controller, string methods, P parameter,
                                               CancellationToken cancellationToken = default, string token = "") where P : class;

        Task<ListResponse<TR>> GetListAsync(string servicePrefix, string version, string controller, string methods, string parameter,
                                            CancellationToken cancellationToken = default, string token = "");

        Task<TokenResponse> GetTokenAsync(string servicePrefix, string version, string controller, string methods, TokenRequest tokenRequest,
                                          CancellationToken cancellationToken = default);

        Task<XResponse> PostAsync(string servicePrefix, string version, string controller, string methods, TD model,
                                   CancellationToken cancellationToken = default, string token = "");

        Task<XResponse> PutAsync(string servicePrefix, string version, string controller, string methods, TD model,
                                 CancellationToken cancellationToken = default, string token = "");

        Task<XResponse> DeleteAsync(string servicePrefix, string version, string controller, string methods, int id,
                                    CancellationToken cancellationToken = default, string token = "");

        Task<XResponse> DeleteAsync(string servicePrefix, string version, string controller, string methods, Guid id,
                                   CancellationToken cancellationToken = default, string token = "");
    }
}