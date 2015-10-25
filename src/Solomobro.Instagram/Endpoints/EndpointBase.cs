using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class EndpointBase
    {
        private readonly IApiClient ApiClient;
        protected readonly string AccessToken;
        protected const string Self = "self";

        internal EndpointBase(string accessToken)
        {
            AccessToken = accessToken;
            ApiClient = Ioc.Resolve<IApiClient>() ?? new ApiClient();
        }

        protected async Task<ObjectResponse<T>> GetObjectResponseAsync<T>(Uri uri)
        {
            return await ApiClient.GetAsync<ObjectResponse<T>>(uri).ConfigureAwait(false);
        }

        protected async Task<CollectionResponse<T>> GetCollectionResponseAsync<T>(Uri uri)
        {
            return await ApiClient.GetAsync<CollectionResponse<T>>(uri).ConfigureAwait(false);
        }

        protected async Task<ObjectResponse<T>> PostObjectResponseAsync<T>(Uri uri, HttpContent content)
        {
            return await ApiClient.PostAsync<ObjectResponse<T>>(uri, content).ConfigureAwait(false);
        }

        
    }
}
