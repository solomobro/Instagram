using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram.Authentication
{
    public class ImplicitAuth : OAuthBase
    {
        private readonly AuthUriBuilder _uriBuilder;


        public override Uri AuthenticationUri => _uriBuilder.BuildImplicitAuthUri();

        public ImplicitAuth(string clientId, string clientSecret, string redirectUri, IEnumerable<string> scopes = null ) : base(clientId, clientSecret, redirectUri)
        {
            _uriBuilder = new AuthUriBuilder(clientId, redirectUri, scopes);
        }

        public  AuthenticationResult AuthenticateFromResponse(Uri instagramResponseUri)
        {
            try
            {
                var accessToken = instagramResponseUri.ExtractAccessToken();
                AuthenticateWithAccessToken(accessToken);
                return new AuthenticationResult
                {
                    Success = true
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
    }
}
