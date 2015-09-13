using System;
using System.Web;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram.Authentication
{
    /// <summary>
    /// Internal-only class to validate and extract data from URIs we get from Instagram
    /// </summary>
    internal static class UriExtensions
    {
        public static string ExtractAccessCode(this Uri uri)
        {
            uri.EnsureSuccessfulAuthReply();

            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            return queryParams.Get("code");
        }

        public static string ExtractAccessToken(this Uri uri)
        {
            uri.EnsureSuccessfulAuthReply();

            var parts = uri.Fragment.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 || parts[0] != "#access_token")
            {
                throw new ArgumentException($"bad uri fragment - {uri.Fragment}");
            }

            return parts[1];
        }

        private static void EnsureSuccessfulAuthReply(this Uri uri)
        {
            /*
                If your request for approval is denied by the user, then we will redirect the user to your redirect_uri with the following parameters:

                http://your-redirect-uri?error=access_denied&error_reason=user_denied&error_description=The+user+denied+your+request
            */

            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            var error = queryParams.Get("error");
            if (error != null)
            {
                var errorReason = queryParams.Get("error_reason");
                if (errorReason.Equals("user_denied", StringComparison.OrdinalIgnoreCase))
                {
                    throw new AccessDeniedException();
                }

                throw new OAuthException(errorReason);
            }
        }
    }
}
