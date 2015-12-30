using System;

namespace Solomobro.Instagram.Authentication
{
    public class OAuthException : Exception
    {
        public string ErrorType { get; private set; }

        internal OAuthException(string message, string errorType) : base(message)
        {
            ErrorType = errorType;
        }
    }
}
