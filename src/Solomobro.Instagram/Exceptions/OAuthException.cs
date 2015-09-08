using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Exceptions
{
    public class OAuthException : Exception
    {
        internal OAuthException(string message) : base(message) { }
    }
}
