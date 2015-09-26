using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class UserStats
    {
        internal UserStats() { }

        [DataMember(Name = "media")]
        public int Media { get; internal set; }

        [DataMember(Name = "follows")]
        public int Follows { get; internal set; }

        [DataMember(Name = "followed_by")]
        public int FollowedBy { get; set; }
    }
}