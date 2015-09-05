using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        private Uri BuildAuthorizationUri()
        {
            var clientId = _authConfig.ClientId;
            var redirectUri = _authConfig.RedirectUri;
            var responseCode = BuildResponseType();
            var scopes = BuildScope();

            //todo: this may need url encodingor escaping, especially in building the scope
            var url = $"{BaseAuthUrl}/authorize/?client_id={clientId}&redirect_uri={redirectUri}&response_type={responseCode}&scope={scopes}";

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
                    throw new ArgumentOutOfRangeException();
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
