using System;
using System.Collections.Generic;
using System.Net.Http;
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
        /// Implements ET /users/{user-id}
        /// </summary>
        public async Task<ObjectResponse<User>> GetAsync(string userId = Api.Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/feed
        /// </summary>
        public async Task<CollectionResponse<Post>> GetFeedAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/feed?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/{user-id}/media/recent
        /// </summary>
        public async Task<CollectionResponse<Post>> GetMediaRecentAsync(string userId = Api.Self)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/media/recent?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/media/liked
        /// </summary>
        public async Task<CollectionResponse<Post>> GetMediaLikedAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/media/liked?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/search
        /// </summary>
        public async Task<CollectionResponse<User>> SearchAsync(string query)
        {
            var uri = new Uri($"{EndpointUri}/search?access_token={_accessToken}&q={query}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        #region Relationships

        internal async Task<CollectionResponse<User>> GetFollowsAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/follows/?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        internal async Task<CollectionResponse<User>> GetFollowedByAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/followed-by?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        internal async Task<CollectionResponse<User>> GetRequestedByAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/requested-by?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        internal async Task<ObjectResponse<RelationShip>> GetRelationshipAsync(string userId)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/relationship?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<RelationShip>(uri).ConfigureAwait(false);
        }

        internal async Task<ObjectResponse<RelationShip>> PostRelationshipAsync(string userId, string action)
        {
            var uri = new  Uri($"{EndpointUri}/{userId}/relationship?access_token={_accessToken}");
            var data = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("action", action),
                }
            );

            return await _endpointBase.PostObjectResponseAsync<RelationShip>(uri, data).ConfigureAwait(false);
        }

        #endregion Relationships 
    }
}
