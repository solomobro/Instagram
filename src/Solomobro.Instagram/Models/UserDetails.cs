using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class UserDetails : User
    {
        internal UserDetails() { }

        [DataMember(Name = "bio")]
        public string Bio { get; internal set; }

        [DataMember(Name = "website")]
        public string Website { get; internal set; }

        [DataMember(Name = "counts")]
        public UserStats Counts { get; internal set; }
    }
}