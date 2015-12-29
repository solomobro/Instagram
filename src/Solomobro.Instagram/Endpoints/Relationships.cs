using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Relationships
    {
        private const string EndpointUri = "https://api.instagram.com/v1/users";
        private readonly IApiClient _apiClient;
        private readonly string _accessToken;

        internal Relationships(IApiClient apiClient, string accessToken)
        {
            _apiClient = apiClient;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /users/self/follows
        /// </summary>
        public async Task<CollectionResponse<User>> GetFollowsAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/follows/?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/followed-by
        /// </summary>
        public async Task<CollectionResponse<User>> GetFollowedByAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/followed-by?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/requested-by
        /// </summary>
        public async Task<CollectionResponse<User>> GetRequestedByAsync()
        {
            var uri = new Uri($"{EndpointUri}/self/requested-by?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/{user-id}/relationship
        /// </summary>
        /// <param name="userId"></param>
        public async Task<Response<RelationShip>> GetRelationshipAsync(string userId)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/relationship?access_token={_accessToken}");
            return await _apiClient.GetResponseAsync<RelationShip>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements  POST /users/{user-id}/relationship
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <param name="action">either one of: follow|unfollow|ignore|approve</param>
        /// <returns></returns>
        public async Task<Response<RelationShip>> ModifyRelationshipAsync(string userId, string action)
        {
            var uri = new Uri($"{EndpointUri}/{userId}/relationship?access_token={_accessToken}");
            var data = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("action", action),
                }
            );

            return await _apiClient.PostResponseAsync<RelationShip>(uri, data).ConfigureAwait(false);
        }
    }
}
