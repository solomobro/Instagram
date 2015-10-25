using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Users : EndpointBase
    {

        
        private const string BaseEndpointUri = "https://api.instagram.com/v1/users";

        internal Users(string accessToken) : base(accessToken)
        {
        }

        /// <summary>
        /// Implements /users/{user-id}
        /// </summary>
        /// <param name="userId">a user id or the string "self" for the authenticated user</param>
        /// <returns>Basic information about the user</returns>
        public async Task<ObjectResponse<UserDetails>> GetUserInfoAsync(string userId = Self)
        {
            var uri = new Uri($"{BaseEndpointUri}/{userId}/?access_token={AccessToken}");
            return await GetObjectResponseAsync<UserDetails>(uri).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/feed
        /// </summary>
        /// <returns>The authenticated user's feed</returns>
        public async Task<CollectionResponse<Post>> GetUserFeedAsync()
        {
            var uri = new Uri($"{BaseEndpointUri}/self/feed?access_token={AccessToken}");
            return await GetCollectionResponseAsync<Post>(uri).ConfigureAwait(false);
        }

        
    }
}
