using System;

namespace Solomobro.Instagram.Exceptions
{
    /// <summary>
    /// Thrown for Instagram error=access_denied. 
    /// The user denied your access request.
    /// </summary>
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() : this("The user denied your request") { }

        public AccessDeniedException(string message) : base(message) { }
    }
}
