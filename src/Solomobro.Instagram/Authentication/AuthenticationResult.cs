using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Authentication
{
    public class AuthenticationResult
    {
        public bool Success { get; internal set; }

        public string Message { get; internal set; }

        /// <summary>
        /// The Authenticated user's basic info. Available only with Server-side/Explicit authentication
        /// </summary>
        public User User { get; internal set; }
    }
}
