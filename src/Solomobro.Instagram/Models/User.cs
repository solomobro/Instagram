using System;
using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    [KnownType(typeof(UserDetails))]
    [KnownType(typeof(UserSearchResult))]
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
    }
}
