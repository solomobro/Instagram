using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Solomobro.Instagram.Interfaces
{
    /// <summary>
    /// Responsible for redirecting user to instagram login/authorization page.
    /// The client must implement this interface.
    /// </summary>
    public interface IInteractiveAuthorizationProvider
    {
        /// <summary>
        /// Process an interactive login session
        /// </summary>
        /// <param name="uri">the URI to Instagram Auth page with all parameters filled in</param>
        /// <returns>a response from Instagram's Auth page indicating whether or not user granted access request</returns>
        Task<HttpResponseMessage> ProcessAuthorizationAsync(Uri uri);
    }
}
