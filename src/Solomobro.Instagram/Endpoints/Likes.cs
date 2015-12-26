using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Likes
    {
        private readonly Media _media;

        internal Likes(Media mediaEndpoint)
        {
            _media = mediaEndpoint;
        }

        /// <summary>
        /// Implements GET /medial/{media-id}/likes
        /// </summary>
        /// <returns>collection of users that like this media</returns>
        public async Task<CollectionResponse<User>> GetAsync(string mediaId)
        {
            return await _media.GetLikesAsync(mediaId).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements POST /media/{media-id}/likes
        /// Sets a like on a post by the authenticated user
        /// </summary>
        public async Task<Response> PostAsync(string mediaId)
        {
            return await _media.PostLikeAsync(mediaId).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements DEL /media/{media-id}/likes
        /// Remove a like on a post by the authenticated user
        /// </summary>
        public async Task<Response> DeleteAsync(string mediaId)
        {
            return await _media.DeleteLikeAsync(mediaId).ConfigureAwait(false);
        } 
    }
}
