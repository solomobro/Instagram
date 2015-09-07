using System;

namespace Solomobro.Instagram.Exceptions
{
    /// <summary>
    /// Thrown for Instagram error=access_denied. 
    /// The user denied your access request.
    /// </summary>
    public class AccessDeniedException : Exception
    {
        internal AccessDeniedException() : this("The user denied your request") { }

        internal AccessDeniedException(string message) : base(message) { }
    }
}
