using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
