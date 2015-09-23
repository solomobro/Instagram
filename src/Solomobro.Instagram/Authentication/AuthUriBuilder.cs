using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram.Authentication
{
    /// <summary>
    /// an internal-only class used to build auth urls
    /// </summary>
    internal class AuthUriBuilder
    {
        private readonly string _clientId;
        private readonly string _redirectUri;
        private readonly HashSet<string> _scopes;

        private const string ExplicitBaseUri = "https://api.instagram.com";
        private const string ImplicitBaseUri = "https://instagram.com";
        private const string ExplicitResponseType = "code";
        private const string ImplicitResponseType = "token";

        public AuthUriBuilder(string clientId, string redirectUri, IEnumerable<string> scopes )
        {
            _clientId = clientId;
            _redirectUri = redirectUri;
            _scopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            AddScopes(scopes);

        }

        public Uri BuildExplicitAuthUri()
        {
            var scope = BuildScope();

            var uri = $"{ExplicitBaseUri}/oauth/authorize/?client_id={_clientId}&redirect_uri={_redirectUri}&response_type={ExplicitResponseType}&scope={scope}";

            return new Uri(uri);
        }

        public Uri BuildImplicitAuthUri()
        {
            var scope = BuildScope();

            var uri = $"{ImplicitBaseUri}/oauth/authorize/?client_id={_clientId}&redirect_uri={_redirectUri}&response_type={ImplicitResponseType}&scope={scope}";

            return new Uri(uri);
        }

        public static Uri BuildAccessCodeUri()
        {
            return new Uri($"{ExplicitBaseUri}/oauth/access_token");
        }

        private void AddScopes(IEnumerable<string> scopes)
        {
            if (scopes == null)
            {
                return;
            }

            foreach (var scope in scopes)
            {
                if (scope.Equals(AccessScope.Basic, StringComparison.OrdinalIgnoreCase))
                {
                    continue; // basic scope. no need to add explicitly
                }
                _scopes.Add(scope); 
            }
        }

        private string BuildScope()
        {
            var sb = new StringBuilder(AccessScope.Basic);

            if (_scopes != null)
            {
                foreach (var scope in _scopes.OrderBy(s => s))
                {
                    sb.Append("+");
                    sb.Append(scope.ToLower());
                }
            }

            return sb.ToString();
        }
    }
}
