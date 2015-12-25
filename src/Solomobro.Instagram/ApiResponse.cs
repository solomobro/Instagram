using Solomobro.Instagram.Models;

namespace Solomobro.Instagram
{
    internal class ApiResponse<T>
    {
        public RateLimit RateLimit { get; internal set; }

        public T Data { get; set; }
    }
}
