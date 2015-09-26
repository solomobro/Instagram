using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class MediaInfoCollection
    {
        [DataMember(Name = "low_resolution")]
        public MediaInfo LowResolution { get; internal set; }

        [DataMember(Name = "standard_resolution")]
        public MediaInfo StandardResolution { get; internal set; }

        [DataMember(Name = "thumbnail")]
        public MediaInfo Thumbnail { get; internal set; }
    }
}
