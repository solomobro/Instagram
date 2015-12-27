using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
