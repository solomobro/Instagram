using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
        /// Implements GET /media/{media-id}
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

        /// <summary>
        /// Implments /media/search
        /// </summary>
        /// <param name="req">Parameters for search query</param>
        /// <returns></returns>
        public async Task<CollectionResponse<Post>> SearchAsync(SearchMediaRequest req)
        {
            var uri = new Uri($"{EndpointUri}/search/?access_token={_accessToken}&lat={req.Latitude}&lng={req.Longitude}&distance={req.DistanceMeters}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        #region comments

        internal async Task<CollectionResponse<Comment>> GetCommentsAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/comments?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Comment>(uri);
        }

        internal async Task<Response> PostCommentAsync(string mediaId, string text)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}?access_token={_accessToken}");
            var data = new FormUrlEncodedContent(
                new []{new KeyValuePair<string, string>("text", text), }
            );

            return await _endpointBase.PostResponseAsync(uri, data).ConfigureAwait(false);
        }

        internal async Task<Response> DeleteCommentAsync(string mediaId, string commentId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/comments/{commentId}?access_token={_accessToken}");
            return await _endpointBase.DeleteResponseAsync(uri).ConfigureAwait(false);

        }

        #endregion comments

        #region Likes

        internal async Task<CollectionResponse<User>> GetLikesAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<User>(uri).ConfigureAwait(false);
        }

        internal async Task<Response> PostLikeAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _endpointBase.PostResponseAsync(uri, null).ConfigureAwait(false);
        }

        internal async Task<Response> DeleteLikeAsync(string mediaId)
        {
            var uri = new Uri($"{EndpointUri}/{mediaId}/likes?access_token={_accessToken}");
            return await _endpointBase.DeleteResponseAsync(uri).ConfigureAwait(false);
        }

        #endregion Likes


    }
}
