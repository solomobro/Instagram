using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    class CollectionResponse<T> : ObjectCollection<T>, IResponse
    {
        internal CollectionResponse() { }

        public override int Count => Data?.Count ?? 0;

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public int RateLimit { get; internal set; }


        [DataMember(Name = "pagination")]
        internal Pagination Pagination { get; set; }
    }
}
