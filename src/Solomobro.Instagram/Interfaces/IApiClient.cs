using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IApiClient
    {
        Task<ApiResponse<T>> GetAsync<T>(Uri uri);

        Task<ApiResponse<T>> PostAsync<T>(Uri uri, HttpContent content);

        Task<ApiResponse<T>> PutAsync<T>(Uri uri, HttpContent content);

        Task<ApiResponse<T>> DeleteAsync<T>(Uri uri);
    }
}
