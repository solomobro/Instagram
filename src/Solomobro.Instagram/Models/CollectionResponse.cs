using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    class CollectionResponse<T> : Response
    {
        public IReadOnlyList<T> Data => DataInternal.AsReadOnly();

        [DataMember(Name = "data")]
        internal List<T> DataInternal;

        public async Task<CollectionResponse<T>> GetNextResultAsync()
        {
            throw new NotImplementedException();
        }
    }
}
