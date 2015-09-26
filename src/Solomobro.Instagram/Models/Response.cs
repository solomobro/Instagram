using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public abstract class Response
    {
        internal Response() { }

        public int RateLimit { get; internal set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        [DataMember(Name = "pagination")]
        internal Pagination Pagination { get; set; }
    }
}
