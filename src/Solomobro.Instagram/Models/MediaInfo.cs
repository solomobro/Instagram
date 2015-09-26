using System;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class MediaInfo
    {
        internal MediaInfo() { }

        public Uri Url => new Uri(UrlInternal);

        [DataMember(Name = "url")]
        internal string UrlInternal;

        [DataMember(Name = "width")]
        public int Width { get; internal set; }

        [DataMember(Name = "height")]
        public int Height { get; internal set; }
    }
}
