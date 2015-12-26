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

        internal async Task<ObjectResponse<T>> GetObjectResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.GetAsync<ObjectResponse<T>>(uri).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.GetAsync<CollectionResponse<T>>(uri).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<ObjectResponse<T>> PostObjectResponseAsync<T>(Uri uri, HttpContent content)
        {
            var resp =  await _apiClient.PostAsync<ObjectResponse<T>>(uri, content).ConfigureAwait(false);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        }

        internal async Task<ObjectResponse<T>> DeleteObjectResponseAsync<T>(Uri uri)
        {
            var resp = await _apiClient.DeleteAsync<ObjectResponse<T>>(uri);
            resp.Data.RateLimit = resp.RateLimit;
            return resp.Data;
        } 
    }
}
