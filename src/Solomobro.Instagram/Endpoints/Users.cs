using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Users 
    {   
        private const string EndpointUri = "https://api.instagram.com/v1/users";
        private readonly IApiClient _apiClient;
        private readonly string _accessToken;

        internal const string Self = "self";

        internal Users(IApiClient apiClient, string accessToken)
        {
            _apiClient = apiClient;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements ET /users/{user-id}
        /// </summary>
        public async Task<Response<User>> GetAsync(string userId = Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/?access_token={_accessToken}");
            return await _apiClient.GetResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/feed
        /// </summary>
        public async Task<CollectionResponse<Post>> GetFeedAsync()
        {
            var uri = new Uri($"{EndpointUri}/{Self}/feed?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/{user-id}/media/recent
        /// </summary>
        public async Task<CollectionResponse<Post>> GetRecentMediaAsync(string userId = Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/media/recent?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/media/liked
        /// </summary>
        public async Task<CollectionResponse<Post>> GetLikedMediaAsync()
        {
            var uri = new Uri($"{EndpointUri}/{Self}/media/liked?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/search
        /// </summary>
        public async Task<CollectionResponse<User>> SearchAsync(string query)
        {
            var uri = new Uri($"{EndpointUri}/search?access_token={_accessToken}&q={query}");
            return await _apiClient.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }
    }
}
