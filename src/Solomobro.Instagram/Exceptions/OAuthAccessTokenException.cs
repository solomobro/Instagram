using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
