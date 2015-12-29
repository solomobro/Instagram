using System.Runtime.Serialization;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Response : IResponse, IRateLimitable
    {
        internal Response() {}

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }

        void IRateLimitable.SetRateLimit(RateLimit limit)
        {
            RateLimit = limit;
        }
    }

    [DataContract]
    public class Response<T> : IResponse, IRateLimitable
    {
        internal Response() { }

        [DataMember(Name = "data")]
        public T Data { get; internal set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }

        void IRateLimitable.SetRateLimit(RateLimit limit)
        {
            RateLimit = limit;
        }
    }
}
