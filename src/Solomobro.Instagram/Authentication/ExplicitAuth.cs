 using System;
using System.Collections.Generic;
 using System.Threading.Tasks;
 using Solomobro.Instagram.Exceptions;
 using Solomobro.Instagram.Extensions;
 using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    public class ExplicitAuth : OAuthBase
    {
        private readonly AuthUriBuilder _uriBuilder;

        public override Uri AuthenticationUri => _uriBuilder.BuildExplicitAuthUri();

        public ExplicitAuth(string clientId, string clientSecret, string redirectUri, IEnumerable<string> scopes = null ) : base(clientId, clientSecret, redirectUri)
        {
            _uriBuilder = new AuthUriBuilder(clientId, redirectUri, scopes);
        }

        public async Task<AuthenticationResult> AuthenticateFromResponseAsync(Uri instagramResponseUri)
        {
            try
            {
                var authInfo = await GetAuthInfoAsync(instagramResponseUri)
                        .ConfigureAwait(false);

                AuthenticateWithAccessToken(authInfo.AccessToken);

                return new AuthenticationResult
                {
                    Success = true,
                    User = authInfo.User,
                };
            }
            catch (AlreadyAuthenticatedException)
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


        private async Task<ExplicitAuthResponse> GetAuthInfoAsync(Uri uri)
        {
            var accessCode = uri.ExtractAuthAccessCode();
            var accessTokenUri = AuthUriBuilder.BuildAccessCodeUri();
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

        private IAccessTokenRetriever GetExplicitAuthenticator(string accessCode)
        {
            return Ioc.Resolve<IAccessTokenRetriever>() ?? new AccessTokenRetriever(ClientId, ClientSecret, RedirectUri, accessCode);
        }
    }
}
