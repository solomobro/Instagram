using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Interfaces
{
    internal interface IRateLimitable
    {
        void SetRateLimit(RateLimit limit);
    }
}
