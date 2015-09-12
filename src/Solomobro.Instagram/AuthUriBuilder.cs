using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram
{
    internal class AuthUriBuilder
    {
        private readonly AuthenticationMethod _authMethod;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly IEnumerable<string> _scopes;

        private const string ExplicitBaseUri = "https://api.instagram.com";
        private const string ImplicitBaseUri = "https://instagram.com";
        private const string ExplicitResponseType = "code";
        private const string ImplicityResponseType = "token";

        public AuthUriBuilder(string clientId, string clientSecret, string redirectUri, AuthenticationMethod method, IEnumerable<string> scopes)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
            _authMethod = method;
            _scopes = scopes;
        }

        public Uri BuildAuthenticationUri()
        {
            string baseUri;
            string responseType;

            switch (_authMethod)
            {
                case AuthenticationMethod.Implicit:
                    baseUri = ImplicitBaseUri;
                    responseType = ImplicityResponseType;
                    break;
                case AuthenticationMethod.Explicit:
                    baseUri = ExplicitBaseUri;
                    responseType = ExplicitResponseType;
                    break;
                default:
                    throw new OAuthException("bad OAuth method");
            }

            var scope = BuildScope();

            var uri = $"{baseUri}/authorize/?client_id={_clientId}&redirect_uri={_redirectUri}&response_type={responseType}&scope={scope}";

            return new Uri(uri);
        }

        private string BuildScope()
        {
            var sb = new StringBuilder(OAuthScope.Basic);

            if (_scopes != null)
            {
                foreach (var scope in _scopes)
                {
                    sb.Append("+");
                    sb.Append(scope);
                }
            }

            return sb.ToString();
        }
    }
}
