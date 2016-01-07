using System.Runtime.Serialization;

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
