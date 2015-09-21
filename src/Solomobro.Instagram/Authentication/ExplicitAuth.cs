 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Solomobro.Instagram.Entities;
 using Solomobro.Instagram.Exceptions;
 using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Authentication
{
    public class ExplicitAuth : OAuthBase
    {
        private readonly AuthUriBuilder _uriBuilder;

        public override Uri AuthenticationUri => _uriBuilder.BuildAuthenticationUri();

        public ExplicitAuth(string clientId, string clientSecret, string redirectUri) : base(clientId, clientSecret, redirectUri)
        {
            _uriBuilder = new AuthUriBuilder(clientId, redirectUri, AuthenticationMethod.Explicit);
        }

        public async Task<AuthenticationResult> AuthenticateFromResponseAsync(Uri instagramResponseUri)
        {
            try
            {
                var authInfo = await GetAuthInfoExplicitAsync(instagramResponseUri)
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
