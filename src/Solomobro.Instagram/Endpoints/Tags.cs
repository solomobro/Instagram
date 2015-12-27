using System;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Tags
    {
        private const string EndpointUri = "https://api.instagram.com/v1/tags";
        private readonly EndpointBase _endpointBase;
        private readonly string _accessToken;

        internal Tags(EndpointBase endpoint, string accessToken)
        {
            _endpointBase = endpoint;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /tags/{tag-name}
        /// </summary>
        public async Task<Response<Tag>> GetAsync(string tagName)
        {
            var uri = new Uri($"{EndpointUri}/{tagName}?access_token={_accessToken}");
            return await _endpointBase.GetResponseAsync<Tag>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /tags/{tag-name}/media/recent
        /// </summary>
        /// <returns>List of recently tagged media</returns>
        public async Task<CollectionResponse<Post>> GetRecentMediaAsync(string tagName)
        {
            var uri = new Uri($"{EndpointUri}/{tagName}/media/recent?access_token={_accessToken}");
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /tags/search
        /// </summary>
        /// <returns>List of tags matching the search query</returns>
        public async Task<CollectionResponse<Tag>> SearchAsync(string query)
        {
            var uri = new Uri($"{EndpointUri}/search?access_token={_accessToken}&q={query}");
            return await _endpointBase.GetCollectionResponseAsync<Tag>(uri).ConfigureAwait(false);
        }
    }
}
