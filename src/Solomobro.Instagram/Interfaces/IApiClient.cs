using System;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IApiClient
    {
        Task<Response> GetResponseAsync(Uri uri);
        Task<Response> PostResponseAsync(Uri uri, HttpContent content);
        Task<Response> DeleteResponseAsync(Uri uri);
        Task<Response<T>> GetResponseAsync<T>(Uri uri);
        Task<Response<T>> PostResponseAsync<T>(Uri uri, HttpContent content);
        Task<Response<T>> DeleteResponseAsync<T>(Uri uri);
        Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri);
        Task<CollectionResponse<T>> PostCollectionResponseAsync<T>(Uri uri, HttpContent content);
        Task<CollectionResponse<T>> DeleteCollectionResponseAsync<T>(Uri uri);
    }
}