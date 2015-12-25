using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Media
    {
        private const string EndpointUri = "https://api.instagram.com/v1/media";
        private readonly EndpointBase _endpointBase;
        private readonly string _accessToken;

        internal Media(EndpointBase endpoint, string accessToken)
        {
            _endpointBase = endpoint;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /media/{media-idea}
        /// </summary>
        public async Task<ObjectResponse<Post>> GetAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implement GET /media/shortcode/{media-id}
        /// </summary>
        public async Task<ObjectResponse<Post>> GetWithShortCodeAsync(string shortCode)
        {
            var uri = new Uri($"{EndpointUri}/shortcode/{shortCode}?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        public async Task<CollectionResponse<Post>> Search(SearchMediaRequest req)
        {
            var uri = new Uri($"{EndpointUri}/search/?access_token={_accessToken}&lat={req.Latitude}&lng={req.Longitude}&distance={req.DistanceMeters}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        } 
    }
}
