using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Defines the OAuth scopes supported by the Instagram API
    /// </summary>
    [DataContract]
    public static class OAuthScope
    {
        [DataMember]
        public const string Basic = "basic";

        [DataMember]
        public const string Comments = "comments";

        [DataMember]
        public const string Relationships = "relationships";

        [DataMember]
        public const string Likes = "likes";
    }
}
