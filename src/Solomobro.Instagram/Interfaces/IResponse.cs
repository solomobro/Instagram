using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Interfaces
{
    public interface IResponse
    {
        Meta Meta { get; }

        int RateLimit { get; }
    }
}
