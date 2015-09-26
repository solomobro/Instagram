using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    class CollectionResponse<T> : Response, IEnumerable<T>
    {
        public IReadOnlyList<T> Data => DataInternal.AsReadOnly();

        [DataMember(Name = "data")]
        internal List<T> DataInternal;

        public async Task<CollectionResponse<T>> GetNextResultAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Data == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
