using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class RelationShip
    {
        internal RelationShip() { }

        [DataMember(Name = "outgoing_status")]
        public string OutgoingStatus { get; internal set; }

        [DataMember(Name = "incoming_status")]
        public string IncomingStatus { get; internal set; }
    }
}
