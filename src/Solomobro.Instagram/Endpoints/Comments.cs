using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Comments
    {
        private Media _media;

        internal Comments(Media endpoint)
        {
            _media = endpoint;
        }

        /// <summary>
        /// implements GET /media/{media-id}/comments
        /// </summary>
        /// <returns>data property is null</returns>
        public async Task<CollectionResponse<Comment>> GetAsync(string mediaId)
        {
            return await _media.GetCommentsAsync(mediaId).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements POST /media/{media-id}comments
        /// </summary>
        public async Task<Response> PostAsync(string mediaId, string text)
        {
            return await _media.PostCommentAsync(mediaId, text).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements DEL /media/{media-id}/comments/{comment-id}
        /// </summary>
        public async Task<Response> DeleteAsync(string mediaId, string commentId)
        {
            return await _media.DeleteCommentAsync(mediaId, commentId).ConfigureAwait(false);
        } 
    }
}
