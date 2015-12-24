using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class ObjectCollection<T> : IEnumerable<T>
    {
        internal ObjectCollection() { }

        [DataMember(Name = "count")]
        public int Count { get; internal set; }        

        [DataMember(Name = "data")]
        internal List<T> DataInternal;

        public IEnumerator<T> GetEnumerator()
        {
            if (DataInternal == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return DataInternal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
