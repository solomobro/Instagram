using System;
using System.Collections.Generic;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Defines the configuration parameters required to authorize your app
    /// </summary>
    public class OAuthConfig
    {
        private readonly HashSet<string> _scopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
        public string ClientId { get; }

        public string RedirectUri { get; }

        /// <summary>
        /// The authentication method to use. Default: Explicit (server-side)
        /// </summary>
        public AuthenticationMethod AuthMethod { get; } = AuthenticationMethod.Explicit;

        public IEnumerable<string> Scopes
        {
            get { return _scopes; }
        }

        /// <summary>
        /// Initialize a new Auth configuration with default authentication method and basic scope
        /// </summary>
        /// <param name="clientId">The client id for your app</param>
        /// <param name="redirectUri">
        /// The URI where the user is redirected after authorization. 
        /// This must match the exact URI registered for your app in the Instagram dev console
        /// </param>
        public OAuthConfig(string clientId, string redirectUri)
        {
            ClientId = clientId;
            RedirectUri = redirectUri;
        }

        /// <summary>
        /// Initialize a new Auth confiuration with basic scope
        /// </summary>
        /// <param name="clientId">The client id for your app</param>
        /// <param name="redirectUri">
        /// The URI where the user is redirected after authorization. 
        /// This must match the exact URI registered for your app in the Instagram dev console
        /// </param>
        /// <param name="authMethod">The authentication flow to use during the authorization process</param>
        public OAuthConfig(string clientId, string redirectUri, AuthenticationMethod authMethod) 
            : this(clientId, redirectUri)
        {
            AuthMethod = authMethod;
        }

        /// <summary>
        /// Add scopes to the configuration
        /// </summary>
        /// <param name="scopes">one or more scopes</param>
        /// <see cref="OAuthScope"/>
        public void AddScopes(params string[] scopes)
        {
            foreach (var scope in scopes)
            {
                _scopes.Add(scope);
            }
        }
    }
}
