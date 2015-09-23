using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IAccessTokenRetriever
    {
        Task<ExplicitAuthResponse> Authenticate(Uri authEndpoint, IEnumerable<KeyValuePair<string, string>> authParams);
    }
}