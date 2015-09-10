using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IAuth
    {
        AuthenticationMethod Method { get; }
        string BaseAuthUrl { get; }
        string Scope { get; }
        string ResponseCode { get; }
        string ClientId { get; set; }
        string RedirectUri { get; set; }
    }
}
