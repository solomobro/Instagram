using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class LocationInfo
    {
        internal LocationInfo() { }

        [DataMember(Name = "id")]
        public long Id { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "latitude")]
        public int Latitude { get; internal set; }

        [DataMember(Name = "longitude")]
        public int Longitude { get; internal set; }
    }
}
