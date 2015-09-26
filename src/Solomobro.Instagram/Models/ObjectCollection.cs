using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class ObjectCollection<T> : IEnumerable<T>
    {
        internal ObjectCollection() { }

        [DataMember(Name = "count")]
        public int Count { get; internal set; }

        [DataMember]
        internal List<T> Data { get; set; } 

        public IEnumerator<T> GetEnumerator()
        {
            if (Data == null)
            {
                return new List<T>().GetEnumerator();
            }

            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
