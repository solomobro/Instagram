using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Media
    {
        private const string EndpointUri = "https://api.instagram.com/v1/media";
        private readonly IApiClient _apiClient;
        private readonly string _accessToken;

        internal Media(IApiClient apiClient, string accessToken)
        {
            _apiClient = apiClient;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /media/{media-id}
        /// </summary>
        public async Task<Response<Post>> GetAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}?access_token={_accessToken}");
            return await _apiClient.GetResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implement GET /media/shortcode/{media-id}
        /// </summary>
        public async Task<Response<Post>> GetWithShortCodeAsync(string shortCode)
        {
            var uri = new Uri($"{EndpointUri}/shortcode/{shortCode}?access_token={_accessToken}");
            return await _apiClient.GetResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implments /media/search
        /// </summary>
        /// <param name="req">Parameters for search query</param>
        /// <returns></returns>
        public async Task<CollectionResponse<Post>> SearchAsync(SearchMediaRequest req)
        {
            var uri = new Uri($"{EndpointUri}/search/?access_token={_accessToken}&lat={req.Latitude}&lng={req.Longitude}&distance={req.DistanceMeters}");
            return await _apiClient.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }
    }
}
