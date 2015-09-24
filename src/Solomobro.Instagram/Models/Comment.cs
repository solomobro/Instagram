using System;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class Comment
    {
        internal Comment() { }

        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        public DateTime CreatedTime 
            => UnixTimeConverter.ConvertFromUnixTime(CreatedTimeInternal);

        [DataMember(Name = "created_time")]
        internal string CreatedTimeInternal;

        [DataMember(Name = "text")]
        public string Text { get; internal set; }

        [DataMember(Name = "from")]
        public User From { get; internal set; }
    }
}
