using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    class ObjectResponse<T> : Response
    {
        internal ObjectResponse() { }

        [DataMember(Name = "data")]
        public T Data { get; internal set; }
    }
}
