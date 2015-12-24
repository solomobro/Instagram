using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Users 
    {   
        private const string EndpointUri = "https://api.instagram.com/v1/users";
        private readonly EndpointBase _endpointBase;
        private readonly string _accessToken;

        internal Users(EndpointBase endpoint, string accessToken)
        {
            _endpointBase = endpoint;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements /users/{user-id}
        /// </summary>
        public async Task<ObjectResponse<User>> GetAsync(string userId = Api.Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/feed
        /// </summary>
        public async Task<CollectionResponse<Post>> GetFeedAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/feed?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/{user-id}/media/recent
        /// </summary>
        public async Task<CollectionResponse<Post>> GetMediaRecentAsync(string userId = Api.Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/media/recent?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/media/liked
        /// </summary>
        public async Task<CollectionResponse<Post>> GetMediaLikedAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/media/liked?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/search
        /// </summary>
        public async Task<CollectionResponse<User>> SearchAsync(string query)
        {
            var uri = new Uri($"{EndpointUri}/search?access_token={_accessToken}&q={query}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }
    }
}
