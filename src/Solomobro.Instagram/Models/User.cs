using System;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class User
    {
        internal User() { }

        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "username")]
        public string UserName { get; internal set; }

        public Uri ProfilePicture => new Uri(ProfilePictureInternal);

        [DataMember(Name = "profile_picture")]
        internal string ProfilePictureInternal;

        [DataMember(Name = "full_name")]
        public virtual string FullName { get; internal set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; internal set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; internal set; }

        [DataMember(Name = "bio")]
        public string Bio { get; internal set; }

        [DataMember(Name = "website")]
        public string Website { get; internal set; }

        [DataMember(Name = "counts")]
        public UserStats Counts { get; internal set; }
    }
}
