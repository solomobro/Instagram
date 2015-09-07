using System;
using System.Collections.Generic;
using System.Linq;
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
        /// The URI to the Instagram authorization endpoint
        /// </summary>
        public Uri AuthorizationUri { get { return _authUri.Value; } }

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
            _authUri = new Lazy<Uri>(BuildAuthorizationUri);
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
        /// Authorizes your client by retrieving the access token from Instagram's reply
        /// to your request to access user data
        /// </summary>
        /// <param name="instagramResponseUri">
        /// The redirect URI with either access code or access token</param>
        /// <returns>An awaitable task</returns>
        public async Task AuthorizeAsync(Uri instagramResponseUri)
        {
            string accessToken;
            switch (AuthMethod)
            {
                case AuthenticationMethod.Implicit:
                    accessToken = await GetAccessTokenImplicitAsync(instagramResponseUri);
                    break;
                case AuthenticationMethod.Explicit:
                    var authInfo = await GetAuthInfoExplicitAsync(instagramResponseUri);
                    accessToken = authInfo.AccessToken;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(AuthMethod), "invalid authentication method");
            }

            AuthorizeWithAccessToken(accessToken);
        }

        /// <summary>
        /// Authorizes your client by retrieving the access token from Instagram's reply
        /// to your request to access user data
        /// </summary>
        /// <param name="instagramResponseUri">
        /// The redirect URI with either access code or access token</param>
        /// <returns>An awaitable task</returns>
        public async Task AuthorizeAsync(string instagramResponseUri)
        {
            await AuthorizeAsync(new Uri(instagramResponseUri));
        }

        /// <summary>
        /// Call this method when you already have an access token for the user
        /// </summary>
        /// <param name="token">the access token</param>
        public void AuthorizeWithAccessToken(string token)
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

        private async Task<string> GetAccessTokenImplicitAsync(Uri uri)
        {
            // todo: this is pure bs: needs testing and assumes auth succeeded
            var fragment = uri.Fragment;
            var token = fragment.Split(new[] {'#'}, StringSplitOptions.RemoveEmptyEntries).Last();
            return await Task.FromResult(token);
        }

        private async Task<ExplicitAuthResponse> GetAuthInfoExplicitAsync(Uri uri)
        {
            // todo: this too is bs - assumes success path and a whole lot of other crap
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            var accessCode = queryParams.Get("code");

            using (var client = new HttpClient(new HttpClientHandler {AllowAutoRedirect = false}))
            {
                var accessTokenUri = new Uri($"{BaseAuthUrl}/access_token");
                var data = JsonConvert.SerializeObject(new
                {
                    client_secret = ClientSecret, grant_type = "authorization_code", redirect_uri = RedirectUri, code = accessCode
                });

                var accessTokenResp = await client.PostAsync(accessTokenUri, new StringContent(data));

                // todo ensure success here

                var userInfo = await accessTokenResp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExplicitAuthResponse>(userInfo);
            }
        }

        private Uri BuildAuthorizationUri()
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
