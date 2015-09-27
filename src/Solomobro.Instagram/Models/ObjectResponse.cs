using System.Runtime.Serialization;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class ObjectResponse<T> : IResponse
    {
        internal ObjectResponse() { }

        [DataMember(Name = "data")]
        public T Data { get; internal set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public int RateLimit { get; internal set; }
    }
}
