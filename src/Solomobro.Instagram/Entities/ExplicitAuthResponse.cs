using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Entities
{
    /// <summary>
    /// The response message of the final stage of explicit authorization
    /// </summary>
    [DataContract]
    internal class ExplicitAuthResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}
