using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Comments
    {
        private const string EndpointUri = "https://api.instagram.com/v1/media";
        private readonly IApiClient _apiClient;
        private readonly string _accessToken;

        internal Comments(IApiClient apiClient, string accessToken)
        {
            _apiClient = apiClient;
            _accessToken = accessToken;
        }

        /// <summary>
        /// implements GET /media/{media-id}/comments
        /// </summary>
        /// <returns>data property is null</returns>
        public async Task<CollectionResponse<Comment>> GetAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/comments?access_token={_accessToken}");
            return await _apiClient.GetCollectionResponseAsync<Comment>(uri);
        }

        /// <summary>
        /// Implements POST /media/{media-id}comments
        /// </summary>
        public async Task<Response> PostAsync(string mediaId, string text)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/comments?access_token={_accessToken}");
            var data = new FormUrlEncodedContent(
                new[] { new KeyValuePair<string, string>("text", text), }
            );

            return await _apiClient.PostResponseAsync(uri, data).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements DEL /media/{media-id}/comments/{comment-id}
        /// </summary>
        public async Task<Response> DeleteAsync(string mediaId, string commentId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/comments/{commentId}?access_token={_accessToken}");
            return await _apiClient.DeleteResponseAsync(uri).ConfigureAwait(false);
        } 
    }
}
