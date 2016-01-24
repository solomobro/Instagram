using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram
{
    internal class ApiClient : IApiClient
    {
        

        private static readonly Lazy<HttpClient> LazyHttpClient = new Lazy<HttpClient>(() =>
        {
            var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            var http = new HttpClient(handler);
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            http.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return http;
        });

        internal ApiClient()
        {
        }

        private HttpClient Http => LazyHttpClient.Value;

        #region Response

        public async Task<Response> GetResponseAsync(Uri uri)
        {
            return await GetAsync<Response>(uri).ConfigureAwait(false);
        }

        public async Task<Response> PostResponseAsync(Uri uri, HttpContent content)
        {
            return await PostAsync<Response>(uri, content).ConfigureAwait(false);
        }

        public async Task<Response> DeleteResponseAsync(Uri uri)
        {
            return await DeleteAsync<Response>(uri).ConfigureAwait(false);
        }
        #endregion


        #region Response<T>

        public async Task<Response<T>> GetResponseAsync<T>(Uri uri)
        {
            return await GetAsync<Response<T>>(uri).ConfigureAwait(false);
        }

        public async Task<Response<T>> PostResponseAsync<T>(Uri uri, HttpContent content)
        {
            return await PostAsync<Response<T>>(uri, content).ConfigureAwait(false);
        }

        public async Task<Response<T>> DeleteResponseAsync<T>(Uri uri)
        {
            return await DeleteAsync<Response<T>>(uri).ConfigureAwait(false);
        }

        #endregion


        #region CollectionResponse<T>

        public async Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri)
        {
            return await GetAsync<CollectionResponse<T>>(uri).ConfigureAwait(false);
        }

        public async Task<CollectionResponse<T>> PostCollectionResponseAsync<T>(Uri uri, HttpContent content)
        {
            return await PostAsync<CollectionResponse<T>>(uri, content).ConfigureAwait(false);
        }

        public async Task<CollectionResponse<T>> DeleteCollectionResponseAsync<T>(Uri uri)
        {
            return await DeleteAsync<CollectionResponse<T>>(uri).ConfigureAwait(false);
        }
        #endregion

        private async Task<T> GetAsync<T>(Uri uri) where T : IRateLimitable
        {
            using (var httpResp = await Http.GetAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<T>().ConfigureAwait(false);
                ((IRateLimitable)apiResp).SetRateLimit(httpResp.GetRateLimitInfo());
                return apiResp;
            }
        }

        private async Task<T> DeleteAsync<T>(Uri uri) where T : IRateLimitable
        {
            using (var httpResp = await Http.DeleteAsync(uri).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<T>().ConfigureAwait(false);
                ((IRateLimitable)apiResp).SetRateLimit(httpResp.GetRateLimitInfo());
                return apiResp;
            }
        }

        private async Task<T> PostAsync<T>(Uri uri, HttpContent content) where T : IRateLimitable
        {
            using (var httpResp = await Http.PostAsync(uri, content).ConfigureAwait(false))
            {
                var apiResp = await httpResp.DeserializeAsync<T>().ConfigureAwait(false);
                ((IRateLimitable)apiResp).SetRateLimit(httpResp.GetRateLimitInfo());
                return apiResp;
            }
        }
    }
}
