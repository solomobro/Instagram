using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Locations
    {
        private const string EndpointUri = "https://api.instagram.com/v1/locations";
        private readonly EndpointBase _endpointBase;
        private readonly string _accessToken;

        internal Locations(EndpointBase endpoint, string accessToken)
        {
            _endpointBase = endpoint;
            _accessToken = accessToken;
        }

        /// <summary>
        /// Implements GET /locations/{location-id}
        /// </summary>
        public async Task<ObjectResponse<Location>> GetAsync(string locationId)
        {
            var uri = new Uri($"{EndpointUri}/{locationId}?access_token={_accessToken}");
            return await _endpointBase.GetObjectResponseAsync<Location>(uri).ConfigureAwait(false);
        }

        public async Task<CollectionResponse<Location>> GetRecentMediaAsync(MediaNearLocationRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
