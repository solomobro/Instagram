using System;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    /// <summary>
    /// A common class wrapped by all endpoints
    /// </summary>
    internal class EndpointBase
    {
        private readonly IApiClient _apiClient;

        internal EndpointBase(string accessToken)
        {
            _apiClient = Ioc.Resolve<IApiClient>() ?? new ApiClient();
        }

        internal async Task<Response<T>> GetResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.GetAsync<Response<T>>(uri).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.GetAsync<CollectionResponse<T>>(uri).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<Response<T>> PostResponseAsync<T>(Uri uri, HttpContent content)
        {
            var resp =  await _apiClient.PostAsync<Response<T>>(uri, content).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<Response<T>> DeleteResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.DeleteAsync<Response<T>>(uri);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<Response> PostResponseAsync(Uri uri, HttpContent content)
        {
            var resp = await _apiClient.PostAsync<Response>(uri, content).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<Response> DeleteResponseAsync(Uri uri)
        {
            var resp = await _apiClient.DeleteAsync<Response>(uri);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        } 
    }
}
