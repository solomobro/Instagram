using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Users : EndpointBase
    {   
        private const string BaseEndpointUri = "https://api.instagram.com/v1/users";

        internal Users(string accessToken) : base(accessToken)
        {
        }

        /// <summary>
        /// Implements /users/{user-id}
        /// </summary>
        public async Task<ObjectResponse<UserDetails>> GetDetailsAsync(string userId = Self)
        {
            var uri = new Uri($"{BaseEndpointUri}/{userId}/?access_token={AccessToken}");
            return await GetObjectResponseAsync<UserDetails>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/feed
        /// </summary>
        public async Task<CollectionResponse<Post>> GetFeedAsync()
        {
            var uri = new Uri($"{BaseEndpointUri}/self/feed?access_token={AccessToken}");
            return await GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/{user-id}/media/recent
        /// </summary>
        public async Task<CollectionResponse<Post>> GetRecentMediaAsync(string userId = Self)
        {
            var uri = new Uri($"{BaseEndpointUri}/{userId}/media/recent?access_token={AccessToken}");
            return await GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/media/liked
        /// </summary>
        public async Task<CollectionResponse<Post>> GetLikedMediaAsync()
        {
            var uri = new Uri($"{BaseEndpointUri}/self/media/liked?access_token={AccessToken}");
            return await GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/search
        /// </summary>
        public async Task<CollectionResponse<UserSearchResult>> SearchAsync(string query)
        {
            var uri = new Uri($"{BaseEndpointUri}/search?access_token={AccessToken}&q={query}");
            return await GetCollectionResponseAsync<UserSearchResult>(uri).ConfigureAwait(false);
        }
    }
}
