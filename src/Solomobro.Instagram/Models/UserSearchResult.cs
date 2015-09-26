using System.Runtime.Serialization;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class UserSearchResult : User
    {
        internal UserSearchResult() { }

        [DataMember(Name = "first_name")]
        public string FirstName { get; internal set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; internal set; }

        public override string FullName => $"{FirstName} {LastName}";
    }
}
