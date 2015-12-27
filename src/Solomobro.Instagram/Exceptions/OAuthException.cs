using System;

namespace Solomobro.Instagram.Exceptions
{
    public class OAuthException : Exception
    {
        internal OAuthException(string message) : base(message) { }
    }
}
