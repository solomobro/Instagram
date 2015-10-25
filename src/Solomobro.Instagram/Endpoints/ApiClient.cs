using System;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;


namespace Solomobro.Instagram.Endpoints
{
    internal class ApiClient : IApiClient
    {
        public async Task<T> GetAsync<T>(Uri uri)
        {
            using (var http = new HttpClient())
            using (var resp = await http.GetAsync(uri).ConfigureAwait(false))
            {
                return await resp.DeserializeAsync<T>();
            }
        }

        public async Task<T> PostAsync<T>(Uri uri, HttpContent content)
        {
            using (var http = new HttpClient())
            using (var resp = await http.PostAsync(uri, content).ConfigureAwait(false))
            {
                return await resp.DeserializeAsync<T>();
            }
        }

        public async Task<T> PutAsync<T>(Uri uri, HttpContent content)
        {
            using (var http = new HttpClient())
            using (var resp = await http.PutAsync(uri, content).ConfigureAwait(false))
            {
                return await resp.DeserializeAsync<T>();
            }
        }

        public async Task<T> DeleteAsync<T>(Uri uri)
        {
            using (var http = new HttpClient())
            using (var resp = await http.DeleteAsync(uri).ConfigureAwait(false))
            {
                return await resp.DeserializeAsync<T>();
            }
        }
    }
}
