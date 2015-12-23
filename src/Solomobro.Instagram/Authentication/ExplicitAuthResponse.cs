using System.Runtime.Serialization;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Authentication
{
    [DataContract]
    internal class ExplicitAuthResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
