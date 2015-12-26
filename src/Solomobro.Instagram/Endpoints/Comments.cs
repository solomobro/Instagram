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
        public async Task<CollectionResponse<Comment>> Get(string mediaId)
        {
            return await _media.GetComments(mediaId).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements POST /media/{media-id}comments
        /// </summary>
        /// <param name="mediaId">media identifier</param>
        /// <param name="text">the comment text</param>
        /// <returns>
        /// todo: maybe just return a meta object here
        /// </returns>
        public async Task<ObjectResponse<Comment>> Post(string mediaId, string text)
        {
            return await _media.PostComment(mediaId, text).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements DEL /media/{media-id}/comments/{comment-id}
        /// </summary>
        /// <param name="mediaId">media identifier</param>
        /// <param name="commentId">comment identifier</param>
        /// <returns>
        /// todo: should probably just return a meta object here
        /// </returns>
        public async Task<ObjectResponse<Comment>> Delete(string mediaId, string commentId)
        {
            return await _media.DeleteComment(mediaId, commentId).ConfigureAwait(false);
        } 
    }
}
