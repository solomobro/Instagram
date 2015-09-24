using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Pagination
    {
        internal Pagination() {};

        public Uri NextUrl => new Uri(NextUrlInternal);

        [DataMember(Name = "next_url")]
        internal string NextUrlInternal;

        [DataMember(Name = "next_max_id")]
        public  string NextMaxId { get; internal set; }
    }
}
