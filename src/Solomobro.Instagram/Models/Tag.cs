using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Tag
    {
        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "media_count")]
        public int MediaCount { get; set; }
    }
}
