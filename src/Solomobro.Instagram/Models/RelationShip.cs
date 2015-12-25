using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
