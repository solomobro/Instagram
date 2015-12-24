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
    public class CollectionResponse<T> : IEnumerable
    {
        internal CollectionResponse() { }

        public int Count => Data?.Count ?? 0;

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public int RateLimit { get; internal set; }


        public IReadOnlyList<T> Data => DataInternal.AsReadOnly();

        [DataMember(Name = "data")]
        internal List<T> DataInternal;

        [DataMember(Name = "pagination")]
        internal Pagination Pagination { get; set; }

        public async Task<CollectionResponse<T>> GetNextResultAsync()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Data == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return Data.GetEnumerator();
        }
    }
}
