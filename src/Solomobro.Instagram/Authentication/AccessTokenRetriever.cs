using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    /// <summary>
    /// Internal-only class to help make OAuth testable by abstracting code that calls instagram over the network
    /// </summary>
    internal class AccessTokenRetriever : IAccessTokenRetriever
    {


        public async Task<ExplicitAuthResponse> AuthenticateAsync(Uri authEndpoint, IEnumerable<KeyValuePair<string, string>> authParams)
        {
            var data = new FormUrlEncodedContent(authParams);

            using (var http = new HttpClient(new HttpClientHandler {AllowAutoRedirect = false}))
            using (var resp = await http.PostAsync(authEndpoint, data).ConfigureAwait(false))
            {
                resp.EnsureSuccessStatusCode();
                var userInfo = await resp.DeserializeAsync<ExplicitAuthResponse>().ConfigureAwait(false);

                return userInfo;
            }
        }
    }
}
