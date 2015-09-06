using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Entities;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram
{
    /// <summary>
    /// The entry point for interacting with the Instagram API
    /// </summary>
    public class Api
    {
        private readonly IInteractiveAuthorizationProvider _authProvider;
        private readonly OAuthConfig _authConfig;
        private string _accessToken;

        private const string BaseAuthUrl = "https://api.instagram.com/oauth";

        public Api(IInteractiveAuthorizationProvider authProvider, OAuthConfig authConfig)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            if (authConfig == null)
            {
                throw new ArgumentNullException(nameof(authConfig));
            }

            _authProvider = authProvider;
            _authConfig = authConfig;
        }

        public async Task AuthorizeAsync()
        {
            try
            {
                var authUri = BuildAuthorizationUri();
                var resp = await _authProvider.ProcessAuthorizationAsync(authUri);

                // todo: find out http code returned when access denied. the api docs don't say
                resp.EnsureSuccessStatusCode(); 

                // get access token depending on specified auth method
                switch (_authConfig.AuthMethod)
                {
                    case AuthenticationMethod.Implicit:
                        _accessToken = await GetAccessTokenImplicitAsync(resp);
                        break;
                    case AuthenticationMethod.Explicit:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_authConfig.AuthMethod));
                }
            }
            catch (Exception ex)
            {
                // todo: throw something more helpful here
                throw;
            }
        }

        private async Task<string> GetAccessTokenImplicitAsync(HttpResponseMessage resp)
        {
            throw new NotImplementedException(); 
        }

        private async Task<ExplicitAuthResponse> GetAccessTokenExplicitAsync(HttpResponseMessage resp)
        {
            throw new NotImplementedException();
        } 

        private Uri BuildAuthorizationUri()
        {
            var clientId = _authConfig.ClientId;
            var redirectUri = _authConfig.RedirectUri;
            var responseCode = BuildResponseType();
            var scope = BuildScope();

            //todo: this may need url encoding or escaping, especially in building the scope
            var url = $"{BaseAuthUrl}/authorize/?client_id={clientId}&redirect_uri={redirectUri}&response_type={responseCode}&scope={scope}";

            return new Uri(url);
        }

        private string BuildResponseType()
        {
            switch (_authConfig.AuthMethod)
            {
                case AuthenticationMethod.Implicit:
                    return "token";
                case AuthenticationMethod.Explicit:
                    return "code";
                default:
                    throw new ArgumentOutOfRangeException(nameof(_authConfig.AuthMethod));
            }
        }

        private string BuildScope()
        {
            var sb = new StringBuilder("default");
            if (_authConfig.Scopes.Any())
            {
                foreach (var scope in _authConfig.Scopes)
                {
                    sb.Append("+");
                    sb.Append(scope);
                }
            }

            return sb.ToString();
        }
    }
}
