using System;
using System.Collections.Generic;
using Solomobro.Instagram.Entities;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IExplicitAuthenticator
    {
        ExplicitAuthResponse Authenticate(Uri authEndpoint, IEnumerable<KeyValuePair<string, string>> authParams);
    }
}