using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    /// <summary>
    /// data structure holding basic info about an Instagram user
    /// </summary>
    [DataContract]
    public class User
    {
        internal User() { }

        [DataMember(Name = "id")]
        public string Id { get; internal set; }

        [DataMember(Name = "username")]
        public string UserName { get; internal set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; internal set; }

        [DataMember(Name = "profile_picture")]
        public string ProfilePicture { get; internal set; }

        [DataMember(Name = "bio")]
        public string Bio { get; internal set; }
        
        [DataMember(Name = "website")]
        public string Website { get; internal set; }

        [DataMember(Name = "counts")]
        public UserStats Counts { get; internal set; }
    }

    [DataContract]
    public class UserStats
    {
        internal UserStats() { }

        [DataMember(Name = "media")]
        public int Media { get; internal set; }

        [DataMember(Name = "follows")]
        public int Follows { get; internal set; }

        [DataMember(Name = "followed_by")]
        public int FollowedBy { get; set; }
    }

}
