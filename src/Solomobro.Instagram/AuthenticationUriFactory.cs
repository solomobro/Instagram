using Solomobro.Instagram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    public class AuthenticationUriFactory
    {
        internal static IAuth BuildAuthenticationUri(AuthenticationMethod method, string redirectUri, string clientId)
        {
            IAuth authMethod;

            switch(method)
            {
                case AuthenticationMethod.Implicit:
                    authMethod = new ImplicitAuth();
                    break;
                default:
                case AuthenticationMethod.Explicit:
                    authMethod = new ExplicitAuth();
                    break;
            }

            //populate properties of IAuth interface
            authMethod.RedirectUri = redirectUri;
            authMethod.ClientId = clientId;

            return authMethod;
        }
    }
}
