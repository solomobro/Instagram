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
        Task<T> GetAsync<T>(Uri uri);

        Task<T> PostAsync<T>(Uri uri, HttpContent content);

        Task<T> PutAsync<T>(Uri uri, HttpContent content);

        Task<T> DeleteAsync<T>(Uri uri);
    }
}
