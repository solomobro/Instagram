using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Authentication
{
    public class AuthenticationResult
    {
        public bool Success { get; internal set; }

        public string Message { get; internal set; }

        public User User { get; internal set; }
    }
}
