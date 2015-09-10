using System;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram
{
    public class ExplicitAuth : IAuth
    {
        public AuthenticationMethod Method { get; } = AuthenticationMethod.Explicit;
        public string BaseAuthUrl { get; } = "https://api.instagram.com";
        public string ResponseCode { get; } = "code";
        public string Scope { get; } = new ScopeBuilder().BuildScope();
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
}
