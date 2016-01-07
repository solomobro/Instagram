using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    public class UserInPhoto
    {
        internal UserInPhoto() { }
        [DataMember(Name = "user")]
        public User User { get; internal set; }

        [DataMember(Name = "position")]
        public Coordinates Position { get; internal set; }
    }
}
