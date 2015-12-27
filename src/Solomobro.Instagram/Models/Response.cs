using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Response : IResponse
    {
        internal Response() {}

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }
    }

    [DataContract]
    public class Response<T> : IResponse
    {
        internal Response() { }

        [DataMember(Name = "data")]
        public T Data { get; internal set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }
    }
}
