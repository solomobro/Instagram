using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class ObjectResponse<T> 
    {
        internal ObjectResponse() { }

        [DataMember(Name = "data")]
        public T Data { get; internal set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }
    }
}
