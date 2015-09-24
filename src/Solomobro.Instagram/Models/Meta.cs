using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Meta
    {
        internal Meta() {}

        [DataMember(Name = "code")]
        public int Code { get; internal set; }

        [DataMember(Name = "error_type")]
        public string ErrorType { get; internal set; }

        [DataMember(Name = "error_message")]
        public string ErrorMessage { get; internal set; }
    }
}
