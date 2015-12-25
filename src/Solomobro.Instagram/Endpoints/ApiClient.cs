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
        private readonly HttpClient _http = new HttpClient();

        public async Task<ApiResponse<T>> GetAsync<T>(Uri uri)
        {
            using (var resp = await _http.GetAsync(uri).ConfigureAwait(false))
            {
                return new ApiResponse<T>
                {
                    Data = await resp.DeserializeAsync<T>(),
                    RateLimit = new RateLimit
                    {
                        Max = resp.GetRateLimitMax(),
                        Remaining = resp.GetRateLimitRemaining()
                    }
                };
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(Uri uri, HttpContent content)
        {
            using (var resp = await _http.PostAsync(uri, content).ConfigureAwait(false))
            {
                return new ApiResponse<T>
                {
                    Data = await resp.DeserializeAsync<T>(),
                    RateLimit = new RateLimit
                    {
                        Max = resp.GetRateLimitMax(),
                        Remaining = resp.GetRateLimitRemaining()
                    }
                };
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T>(Uri uri, HttpContent content)
        {
            using (var resp = await _http.PutAsync(uri, content).ConfigureAwait(false))
            {
                return new ApiResponse<T>
                {
                    Data = await resp.DeserializeAsync<T>(),
                    RateLimit = new RateLimit
                    {
                        Max = resp.GetRateLimitMax(),
                        Remaining = resp.GetRateLimitRemaining()
                    }
                };
            }
        }

        public async Task<ApiResponse<T>>  DeleteAsync<T>(Uri uri)
        {
            using (var resp = await _http.DeleteAsync(uri).ConfigureAwait(false))
            {
                return new ApiResponse<T>
                {
                    Data = await resp.DeserializeAsync<T>(),
                    RateLimit = new RateLimit
                    {
                        Max = resp.GetRateLimitMax(),
                        Remaining = resp.GetRateLimitRemaining()
                    }
                };
            }
        }

    }
}
