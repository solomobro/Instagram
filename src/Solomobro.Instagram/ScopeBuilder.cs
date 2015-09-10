using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    public class ScopeBuilder
    {
        private readonly HashSet<string> _scopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public IEnumerable<string> Scopes
        {
            get { return _scopes; }
        }
        /// <summary>
        /// Add scopes to the configuration
        /// </summary>
        /// <param name="scopes">one or more scopes</param>
        /// <see cref="OAuthScope"/>
        private void AddScopes(params string[] scopes)
        {
            foreach (var scope in scopes)
            {
                _scopes.Add(scope);
            }
        }

        /// <summary>
        ///  Build the scope of this URI
        /// </summary>
        /// <returns>scope</returns>
        public string BuildScope()
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
