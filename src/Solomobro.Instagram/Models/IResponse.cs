using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Models
{
    internal interface IResponse
    {

         Meta Meta { get; }

        RateLimit RateLimit { get; }
    }
}
