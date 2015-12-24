using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    internal class ApiResponse<T>
    {
        public int RateLimitMax { get; set; }

        public int RateLimitRemaining { get; set; }

        public T Data { get; set; }
    }
}
