using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solomobro.Instagram.Entities;
using Solomobro.Instagram.Exceptions;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    /// <summary>
    /// Helps authenticate and authorize your instagram client to access user data
    /// </summary>
    public abstract class OAuthBase
    {
        private string _accessToken;

        public string ClientId { get; }

        public string ClientSecret { get; }

        public string RedirectUri { get; }

        public abstract Uri AuthenticationUri { get; }

        /// <summary>
        /// True if object has an access token
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_accessToken);



        /// <summary>
        /// Initialize a new Auth confiuration with basic scope
        /// </summary>
        /// <param name="clientId">The client id for your app</param>
        /// <param name="clientSecret">The client secret for your app</param>
        /// <param name="redirectUri">
        /// The URI where the user is redirected after authorization. 
        /// This must match the exact URI registered for your app in the Instagram dev console
        /// </param>
        protected internal OAuthBase(string clientId, string clientSecret, string redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }



        /// <summary>
        /// Call this method when you already have an access token for the user
        /// </summary>
        /// <param name="token">the access token</param>
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

        /// <summary>
        /// Creates an API with access to authenticated endpoints
        /// </summary>
        /// <returns>An instagram API</returns>
        /// <exception cref="OAuthAccessTokenException">
        /// Thrown when authentication is incomplete and there is no access token
        /// </exception>
        public Api CreateAuthenticatedApi()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                throw new OAuthAccessTokenException("access token is invalid");
            }

            return new Api(ClientId, ClientSecret, _accessToken);
        }

        /// <summary>
        /// Creates an API with access to unauthenticated endpoints only
        /// </summary>
        /// <returns>An Instagram API</returns>
        public Api CreateUnauthenticatedApi()
        {
            return new Api(ClientId, ClientSecret, null);
        }

    }
}
