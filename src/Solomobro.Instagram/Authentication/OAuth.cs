using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Entities;
using Solomobro.Instagram.Exceptions;
using Solomobro.Instagram.Interfaces;
using AuthUriBuilder = Solomobro.Instagram.Authentication.AuthUriBuilder;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Helps authenticate and authorize your instagram client to access user data
    /// </summary>
    public class OAuth
    {
        private readonly Lazy<Uri> _authUri;

        private string _accessToken;

        public string ClientId { get; }

        public string ClientSecret { get; }

        public string RedirectUri { get; }

        private readonly AuthUriBuilder _uriBuilder;

        public AuthenticationMethod AuthMethod { get; }

        /// <summary>
        /// True if object has an access token
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_accessToken);

        /// <summary>
        /// The URI to the Instagram authentication endpoint
        /// </summary>
        public Uri AuthenticationUri => _authUri.Value;


        /// <summary>
        /// Initialize a new Auth confiuration with basic scope
        /// </summary>
        /// <param name="clientId">The client id for your app</param>
        /// <param name="clientSecret">The client secret for your app</param>
        /// <param name="redirectUri">
        /// The URI where the user is redirected after authorization. 
        /// This must match the exact URI registered for your app in the Instagram dev console
        /// </param>
        /// <param name="authMethod">The authentication flow to use during the authorization process</param>
        public OAuth(string clientId, string clientSecret, string redirectUri, AuthenticationMethod authMethod = AuthenticationMethod.Explicit)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            AuthMethod = authMethod;

            var uriBuilder = new AuthUriBuilder(clientId, redirectUri, AuthMethod);
            _uriBuilder = uriBuilder;
            _authUri = new Lazy<Uri>(uriBuilder.BuildAuthenticationUri);
        }


        /// <summary>
        /// Validates your authorization by retrieving the access token from Instagram's reply
        /// to your request to access user data
        /// </summary>
        /// <param name="instagramResponseUri">
        /// The redirect URI with either access code or access token</param>
        /// <returns>A status indicating whether authentication succeeded</returns>
        public async Task<AuthenticationResult> ValidateAuthenticationAsync(Uri instagramResponseUri)
        {
            try
            {
                string accessToken;
                User user = null;

                switch (AuthMethod)
                {
                    case AuthenticationMethod.Implicit:
                        accessToken = GetAccessTokenImplicit(instagramResponseUri);
                        break;
                    case AuthenticationMethod.Explicit:
                        var authInfo = await GetAuthInfoExplicitAsync(instagramResponseUri).ConfigureAwait(false);
                        accessToken = authInfo.AccessToken;
                        user = authInfo.User;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(AuthMethod), "invalid authentication method");
                }

                AuthenticateWithAccessToken(accessToken);

                return new AuthenticationResult
                {
                    Success = true,
                    User = user
                };
            }
            catch (AlreadyAuthorizedException)
            {
                throw; // caller is trying to do something fishy
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = $"[{ex.GetType().FullName}] {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Validates your authorization by retrieving the access token from Instagram's reply
        /// to your request to access user data
        /// </summary>
        /// <param name="instagramResponseUri">
        /// The redirect URI with either access code or access token</param>
        /// <returns>A status indicating whether authentication succeeded</returns>
        public async Task<AuthenticationResult> ValidateAuthenticationAsync(string instagramResponseUri)
        {
            Uri uri;
            try
            {
                uri = new Uri(instagramResponseUri);
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = $"[{ex.GetType().FullName}] {ex.Message}"
                };
            }

            // don't wrap this in a try-catch
            return await ValidateAuthenticationAsync(uri).ConfigureAwait(false);
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
                throw new AlreadyAuthorizedException();
            }

            // basic sanity check on the access token
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("access token cannot be null or empty");
            }

            _accessToken = token;
        }

        /// <summary>
        /// Add a scope parameter to the Authentication URI. 
        /// You must call this before authenticating your client if you want more than basic access
        /// </summary>
        /// <param name="scopes">the scopes to add</param>
        public void AddScope(params string[] scopes)
        {
            foreach (var scope in scopes)
            {
                if (string.IsNullOrWhiteSpace(scope))
                {
                    throw new OAuthException("invalid scope");
                }

                _uriBuilder.AddScope(scope); 
            }
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

        private static string GetAccessTokenImplicit(Uri uri)
        {
            return uri.ExtractAccessToken();
        }

        private async Task<ExplicitAuthResponse> GetAuthInfoExplicitAsync(Uri uri)
        {
            var accessCode = uri.ExtractAccessCode();
            var accessTokenUri = _uriBuilder.BuildAccessCodeUri();
            var authenticator = GetExplicitAuthenticator(accessCode);

            var authParams = new[]
            {
                new KeyValuePair<string, string>("client_id", ClientId),
                new KeyValuePair<string, string>("client_secret", ClientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                new KeyValuePair<string, string>("code", accessCode)
            };

            return await authenticator.Authenticate(accessTokenUri, authParams).ConfigureAwait(false);
        }

        private IExplicitAuthenticator GetExplicitAuthenticator(string accessCode)
        {
            return Ioc.Resolve<IExplicitAuthenticator>() ?? new ExplicitAuthenticator(ClientId, ClientSecret, RedirectUri, accessCode);
        }
    }
}
