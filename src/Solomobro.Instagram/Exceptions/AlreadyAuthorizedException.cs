using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Exceptions
{
    /// <summary>
    /// Thrown when client attempts to change an already existing token on an <see cref="OAuth"/> object
    /// </summary>
    public class AlreadyAuthorizedException : Exception
    {
        internal AlreadyAuthorizedException() : base("Previously authorized with an access token. Access token cannot be changed.") { }
        internal AlreadyAuthorizedException(string message) : base(message) { }
    }
}
