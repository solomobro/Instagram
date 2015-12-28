using System;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;


namespace Solomobro.Instagram.Endpoints
{
    internal class ApiClient : IApiClient
    {
        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>(() => new HttpClient());

        internal ApiClient()
        {
        }

        private HttpClient Http => LazyHttpClient.Value;

        #region Response

        public async Task<Response> GetResponseAsync(Uri uri)
        {
            using (var httpResp = await Http.GetAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<Response> PostResponseAsync(Uri uri, HttpContent content)
        {
            using (var httpResp = await Http.PostAsync(uri, content).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<Response> DeleteResponseAsync(Uri uri)
        {
            using (var httpResp = await Http.DeleteAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }
        #endregion


        #region Response<T>

        public async Task<Response<T>> GetResponseAsync<T>(Uri uri)
        {
            using (var httpResp = await Http.GetAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<Response<T>> PostResponseAsync<T>(Uri uri, HttpContent content)
        {
            using (var httpResp = await Http.PostAsync(uri, content).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<Response<T>> DeleteResponseAsync<T>(Uri uri)
        {
            using (var httpResp = await Http.DeleteAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<Response<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        #endregion


        #region CollectionResponse<T>

        public async Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri)
        {
            using (var httpResp = await Http.GetAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<CollectionResponse<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<CollectionResponse<T>> PostCollectionResponseAsync<T>(Uri uri, HttpContent content)
        {
            using (var httpResp = await Http.PostAsync(uri, content).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<CollectionResponse<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }

        public async Task<CollectionResponse<T>> DeleteCollectionResponseAsync<T>(Uri uri)
        {
            using (var httpResp = await Http.DeleteAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<CollectionResponse<T>>().ConfigureAwait(false);
                apiResp.RateLimit = httpResp.GetRateLimitInfo();
                return apiResp;
            }
        }
        #endregion
    }
}
