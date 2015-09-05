using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Solomobro.Instagram.Interfaces
{
    /// <summary>
    /// Responsible for redirecting user to instagram login/authorization page.
    /// The client must implement this interface.
    /// </summary>
    public interface IInteractiveLoginProvider
    {
        /// <summary>
        /// Process an interactive login session
        /// </summary>
        /// <param name="req">a request to Instagram's Auth page requesting access to user account</param>
        /// <returns>a response from Instagram's Auth page indicating whether or not user granted access request</returns>
        Task<HttpResponseMessage> ProcessAuthorizationAsync(HttpRequestMessage req);
    }
}
