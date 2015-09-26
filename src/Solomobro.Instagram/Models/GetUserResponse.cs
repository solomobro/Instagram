using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class GetUserResponse : Response
    {
        internal GetUserResponse() { }

        [DataMember(Name = "data")]
        public UserDetails Data { get; internal set; }
    }
}
