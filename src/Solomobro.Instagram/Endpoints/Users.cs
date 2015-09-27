using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Users
    {
        private readonly string _accessToken;

        private const string Self = "self";
        private const string BaseEndpointUri = "https://api.instagram.com/v1/users";

        internal Users(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<ObjectResponse<UserDetails>> GetUserInfoAsync(string userId = Self)
        {
            var uri = new Uri($"{BaseEndpointUri}/{userId}/?access_token={_accessToken}");

            using (var http = new HttpClient())
            using (var resp = await http.GetAsync(uri).ConfigureAwait(false))
            {
                return await  resp.DeserializeAsync<ObjectResponse<UserDetails>>().ConfigureAwait(false);
            }
        }

        public async Task<CollectionResponse<Post>> GetUserFeedAsync(string userId = "self")
        {
            throw new NotImplementedException();
        }


    }
}
