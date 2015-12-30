using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Extensions;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    public sealed class OAuth
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

        public async Task<AuthenticationResult> AuthenticateExplicitlyAsync(Uri responseUri)
        {
            var accessCode = responseUri.ExtractAuthAccessCode();
            return await AuthenticateExplicitlyAsync(accessCode).ConfigureAwait(false);
        }

        public async Task<AuthenticationResult> AuthenticateExplicitlyAsync(string accessCode)
        {
            var authUri = AuthUriBuilder.BuildAccessCodeUri();
            var authenticator = Ioc.Resolve<IAccessTokenRetriever>() ?? new AccessTokenRetriever();
            var authParams = new Dictionary<string, string>
            {
                ["client_id"] = ClientId,
                ["client_secret"] = ClientSecret,
                ["grant_type"] = "authorization_code",
                ["redirect_uri"] = RedirectUri,
                ["code"] = accessCode
            };

            try
            {
                var authResult = await authenticator.AuthenticateAsync(authUri, authParams).ConfigureAwait(false);

                AuthenticateFromAccessToken(authResult?.AccessToken);

                return new AuthenticationResult
                {
                    Success = true,
                    User = authResult?.User
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
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
