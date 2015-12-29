using System;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram.Authentication
{
    public abstract class OAuthBase
    {
        private string _accessToken;

        internal string ClientId { get; }

        internal string ClientSecret { get; }

        internal string RedirectUri { get; }

        public abstract Uri AuthenticationUri { get; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_accessToken);

        protected internal OAuthBase(string clientId, string clientSecret, string redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        public void AuthenticateWithAccessToken(string token)
        {
            // check that object was not previously authorized
            if (!string.IsNullOrWhiteSpace(this._accessToken))
            {
                throw new AlreadyAuthenticatedException();
            }

            // basic sanity check on the access token
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("access token cannot be null or empty");
            }

            _accessToken = token;
        }

        public Api CreateAuthenticatedApi()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                throw new OAuthAccessTokenException("access token is invalid");
            }

            return new Api(ClientId, ClientSecret, _accessToken);
        }
    }
}
