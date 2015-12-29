using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Likes
    {
        private const string EndpointUri = "https://api.instagram.com/v1/media";
        private readonly IApiClient _apiClient;
        private readonly string _accessToken;

        internal Likes(IApiClient apiClient, string accessToken)
        {
            _apiClient = apiClient;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /medial/{media-id}/likes
        /// </summary>
        /// <returns>collection of users that like this media</returns>
        public async Task<CollectionResponse<User>> GetAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements POST /media/{media-id}/likes
        /// Sets a like on a post by the authenticated user
        /// </summary>
        public async Task<Response> PostAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _apiClient.PostResponseAsync(uri, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements DEL /media/{media-id}/likes
        /// Remove a like on a post by the authenticated user
        /// </summary>
        public async Task<Response> DeleteAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _apiClient.DeleteResponseAsync(uri).ConfigureAwait(false);
        } 
    }
}
