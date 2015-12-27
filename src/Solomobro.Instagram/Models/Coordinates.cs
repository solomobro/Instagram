using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Coordinates
    {
        internal Coordinates() { }

        [DataMember(Name = "x")]
        public float X { get; internal set; }

        [DataMember(Name = "Y")]
        public float Y { get; internal set; }
    }
}
