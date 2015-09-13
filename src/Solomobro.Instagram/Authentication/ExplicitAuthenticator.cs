using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solomobro.Instagram.Entities;
using Solomobro.Instagram.Exceptions;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    /// <summary>
    /// Internal-only class to help make OAuth testable by abstracting code that calls instagram over the network
    /// </summary>
    internal class ExplicitAuthenticator : IExplicitAuthenticator
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly string _accessCode;

        public ExplicitAuthenticator(string clientId, string clientSecret, string redirectUri, string accessCode)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
            _accessCode = accessCode;
        }

        public async Task<ExplicitAuthResponse> Authenticate(Uri authEndpoint, IEnumerable<KeyValuePair<string, string>> authParams)
        {
            var data = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("redirect_uri", _redirectUri),
                    new KeyValuePair<string, string>("code", _accessCode)
                });

            using (var http = new HttpClient(new HttpClientHandler {AllowAutoRedirect = false}))
            {
                var resp = await http.PostAsync(authEndpoint, data).ConfigureAwait(false);
                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                var userInfo = JsonConvert.DeserializeObject<ExplicitAuthResponse>(content);
                if (userInfo == null)
                {
                    throw new OAuthException(content);
                }

                return userInfo;
            }
        }
    }
}
