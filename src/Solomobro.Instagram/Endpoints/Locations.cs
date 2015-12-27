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

        /// <summary>
        /// Implements GET /locations/{location-id}/media/recent
        /// </summary>
        /// <param name="locationId">a location ID</param>
        /// <param name="options">[optional] search options</param>
        /// <returns>List of recent posts near a given location</returns>
        public async Task<CollectionResponse<Post>> GetRecentMediaAsync(string locationId, MediaSearchOptions options)
        {
            var uriStr = $"{EndpointUri}/{locationId}/media/recent?access_token={_accessToken}";

            if (options != null)
            {
                if (!string.IsNullOrWhiteSpace(options.MinId))
                {
                    uriStr += $"&min_id={options.MinId}";
                }

                if (!string.IsNullOrWhiteSpace(options.MaxId))
                {
                    uriStr += $"&max_id={options.MaxId}";
                }

                if (options.MinTimestamp.HasValue)
                {
                    var date = UnixTimeConverter.ConvertToUnixTime(options.MinTimestamp.Value);
                    uriStr += $"&min_timestamp={date}";
                }

                if (options.MaxTimestamp.HasValue)
                {
                    var date = UnixTimeConverter.ConvertToUnixTime(options.MaxTimestamp.Value);
                    uriStr += $"&min_timestamp={date}";
                }
            }

            var uri = new Uri(uriStr);
            return await _endpointBase.GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /locations/search using latitude and longitude parameters
        /// </summary>
        /// <param name="latitude">lat of center search coordinate</param>
        /// <param name="longitude">lng of center search coordinate</param>
        /// <param name="distanceMeters">default: 1000, max: 5000</param>
        /// <returns>List of locations</returns>
        public async Task<CollectionResponse<Location>> SearchAsync(float latitude, float longitude, int distanceMeters = 1000)
        {
            var uri = new Uri($"{EndpointUri}/search?access_token={_accessToken}&lat={latitude}&lng={longitude}&distance={distanceMeters}");
            return await SearchAsync(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /locations/search using Facebook Places ID
        /// </summary>
        /// <param name="fbPlacesId">Location mapped off of FB Places ID</param>
        /// <param name="distanceMeters">default: 1000, max: 5000</param>
        /// <returns>List of locations</returns>
        public async Task<CollectionResponse<Location>> SearchWithFacebookIdAsync(string fbPlacesId, int distanceMeters = 1000)
        {
            var uri = new Uri($"{EndpointUri}/search?acess_token={_accessToken}&facebook_places_id={fbPlacesId}&distance={distanceMeters}");
            return await SearchAsync(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /locations/search using Foursqaure ID (v1 API) [DEPRECATED]
        /// </summary>
        /// <param name="fsqId">Location mapped off of Foursquare ID</param>
        /// <param name="distanceMeters">default: 1000, max: 5000</param>
        /// <returns>List of locations</returns>
        [Obsolete("Use SearchWithFoursquareIdV2Async instead")]
        public async Task<CollectionResponse<Location>> SearchWithFoursquareIdV1Async(string fsqId, int distanceMeters = 1000)
        {
            var uri = new Uri($"{EndpointUri}/search?acess_token={_accessToken}&foursquare_id={fsqId}&distance={distanceMeters}");
            return await SearchAsync(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /locations/search using Foursquare ID using V2 API
        /// </summary>
        /// <param name="fsqId">Location mapped off of Foursquare ID</param>
        /// <param name="distanceMeters">default: 1000, max: 5000</param>
        /// <returns>List of locations</returns>
        public async Task<CollectionResponse<Location>> SearchWithFoursquareIdV2Async(string fsqId, int distanceMeters = 1000)
        {
            var uri = new Uri($"{EndpointUri}/search?acess_token={_accessToken}&foursquare_v2_id={fsqId}&distance={distanceMeters}");
            return await SearchAsync(uri).ConfigureAwait(false);
        }

        private async Task<CollectionResponse<Location>> SearchAsync(Uri uri)
        {
            return await _endpointBase.GetCollectionResponseAsync<Location>(uri).ConfigureAwait(false);
        } 
    }
}
