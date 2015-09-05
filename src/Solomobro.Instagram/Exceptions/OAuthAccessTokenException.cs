using System;

namespace Solomobro.Instagram.Exceptions
{
    /// <summary>
    /// Thrown for Instagram error_type=OAuthAccessTokenError. 
    /// When thrown, you will need to (re-)authorize your application.
    /// </summary>
    public class OAuthAccessTokenException : Exception
    {
        public OAuthAccessTokenException(string message) : base(message) { }
    }
}
