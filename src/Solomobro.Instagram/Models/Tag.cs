using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Tag
    {
        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "media_count")]
        public int MediaCount { get; set; }
    }
}
