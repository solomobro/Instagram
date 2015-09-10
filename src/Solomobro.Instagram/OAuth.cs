using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Solomobro.Instagram.Entities;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Helps authenticate and authorize your instagram client to access user data
    /// </summary>
    public class OAuth
    {
        private const string BaseAuthUrl = "https://api.instagram.com/oauth";
        private readonly HashSet<string> _scopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly Lazy<Uri> _authUri;

        private string _accessToken;
            
        public string ClientId { get; }

        public string ClientSecret { get; }

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
        /// True if object has an access token
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_accessToken);

        /// <summary>
        /// The URI to the Instagram authentication endpoint
        /// </summary>
        public Uri AuthenticationUri { get { return _authUri.Value; } }

        /// <summary>
        /// Initialize a new Auth configuration with default authentication method and basic scope
        /// </summary>
        /// <param name="clientId">The client id for your app</param>
        /// <param name="clientSecret">The clien secret for you app</param>
        /// <param name="redirectUri">
        /// The URI where the user is redirected after authorization. 
        /// This must match the exact URI registered for your app in the Instagram dev console
        /// </param>
        public OAuth(string clientId, string clientSecret, string redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            _authUri = new Lazy<Uri>(BuildAuthenticationUri);
        }

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
        public OAuth(string clientId, string clientSecret, string redirectUri, AuthenticationMethod authMethod) 
            : this(clientId, clientSecret, redirectUri)
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

        /// <summary>
        /// Validates your authorization by retrieving the access token from Instagram's reply
        /// to your request to access user data
        /// </summary>
        /// <param name="instagramResponseUri">
        /// The redirect URI with either access code or access token</param>
        /// <returns>An awaitable task</returns>
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
        /// <returns>An awaitable task</returns>
        public async Task<AuthenticationResult> ValidateAuthenticationAsync(string instagramResponseUri)
        {
            try
            {
                var uri = new Uri(instagramResponseUri);
            }
            catch (Exception ex) // don't catch any other exception here
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = $"[{ex.GetType().FullName}] {ex.Message}"
                };
            }

            // don't wrap this in a try-catch
            return await ValidateAuthenticationAsync(new Uri(instagramResponseUri));
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
        /// Creates an API with access to authenticated endpoints
        /// </summary>
        /// <returns>An instagram API</returns>
        /// <exception cref="OAuthAccessTokenException">
        ///     Thrown when authentication is incomplete and there is no access token
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
            EnsureSuccessUri(uri);

            var parts = uri.Fragment.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 || parts[1] != "#access_token")
            {
                throw new ArgumentException($"bad uri fragment - {uri.Fragment}");
            }

            return parts[1];
        }

        private async Task<ExplicitAuthResponse> GetAuthInfoExplicitAsync(Uri uri)
        {
            EnsureSuccessUri(uri);

            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            var accessCode = queryParams.Get("code");

            using (var client = new HttpClient(new HttpClientHandler {AllowAutoRedirect = false}))
            {
                var accessTokenUri = new Uri($"{BaseAuthUrl}/access_token");
                var data = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("client_secret", ClientSecret),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUri), 
                    new KeyValuePair<string, string>("code", accessCode)    
                });

                var resp = await client.PostAsync(accessTokenUri, data).ConfigureAwait(false);
                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var userInfo =  JsonConvert.DeserializeObject<ExplicitAuthResponse>(content);
                if (userInfo == null)
                {
                    throw new OAuthException(content);
                }

                return userInfo;
            }
        }

        private static void EnsureSuccessUri(Uri uri)
        {
            /*
                If your request for approval is denied by the user, then we will redirect the user to your redirect_uri with the following parameters:

                http://your-redirect-uri?error=access_denied&error_reason=user_denied&error_description=The+user+denied+your+request
            */

            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            var error = queryParams.Get("error");
            if (error != null)
            {
                var errorReason = queryParams.Get("error_reason");
                if (errorReason.Equals("user_denied", StringComparison.OrdinalIgnoreCase))
                {
                    throw new AccessDeniedException();
                }

                throw new OAuthException(errorReason);
            }
        }

        private Uri BuildAuthenticationUri()
        {
            var responseCode = BuildResponseType();
            var scope = BuildScope();

            //todo: this may need url encoding or escaping, especially in building the scope
            var url = $"{BaseAuthUrl}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type={responseCode}&scope={scope}";

            return new Uri(url);
        }

        private string BuildResponseType()
        {
            switch (AuthMethod)
            {
                case AuthenticationMethod.Implicit:
                    return "token";
                case AuthenticationMethod.Explicit:
                    return "code";
                default:
                    throw new ArgumentOutOfRangeException(nameof(AuthMethod));
            }
        }

        private string BuildScope()
        {
            var sb = new StringBuilder(OAuthScope.Basic);
            if (Scopes.Any())
            {
                foreach (var scope in Scopes)
                {
                    sb.Append("+");
                    sb.Append(scope);
                }
            }

            return sb.ToString();
        }
    }
}
