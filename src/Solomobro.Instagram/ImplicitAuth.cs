using System;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram
{
    public class ImplicitAuth : IAuth
    {
        public AuthenticationMethod Method { get; } = AuthenticationMethod.Implicit;
        public string BaseAuthUrl { get; } = "https://instagram.com/oauth";
        public string ResponseCode { get; } = "token";
        public string Scope { get; } = new ScopeBuilder().BuildScope();
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
}
