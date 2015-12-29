using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Authentication
{
    public class OAuth
    {
        private readonly AuthUriBuilder _uriBuilder;

        public string ClientId { get; private set; }
        public string RedirectUri { get; private set; }
        public string ClientSecret { get; private set; }
        public string AccessToken { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken);
        public Uri ExplicitAuthUri => _uriBuilder.BuildExplicitAuthUri();
        public Uri ImplicitAuthUri => _uriBuilder.BuildImplicitAuthUri();

        public OAuth(string clientId, string clientSecret, string redirectUri, params string[] scopes)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;

            _uriBuilder = new AuthUriBuilder(clientId, clientSecret, scopes);
        }

        public void AuthenticateFromAccessToken(string accessToken)
        {
            if (IsAuthenticated)
            {
                throw new InvalidOperationException("Instance already has an access token");
            }

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException(nameof(accessToken) + "cannot be empty or null");
            }

            AccessToken = accessToken;
        }

        public Api CreateApi()
        {
            if (!IsAuthenticated)
            {
                throw new InvalidOperationException("Instance is not authenticated");
            }

            return new Api(ClientId, ClientSecret, AccessToken);
        }
    }
}
