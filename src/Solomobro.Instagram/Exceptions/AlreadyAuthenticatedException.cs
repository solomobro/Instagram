using System;
using Solomobro.Instagram.Authentication;

namespace Solomobro.Instagram.Exceptions
{
    /// <summary>
    /// Thrown when client attempts to change an already existing token on an <see cref="OAuthBase"/> object
    /// </summary>
    public class AlreadyAuthenticatedException : Exception
    {
        internal AlreadyAuthenticatedException() : base("Previously authorized with an access token. Access token cannot be changed.") { }
        internal AlreadyAuthenticatedException(string message) : base(message) { }
    }
}
