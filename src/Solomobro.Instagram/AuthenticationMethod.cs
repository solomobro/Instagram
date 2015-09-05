using System.Runtime.Serialization;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Authentication methods supported by the Instagram API: implicit (client) or explicit (server)
    /// </summary>
    [DataContract]
    public enum AuthenticationMethod
    {
        /// <summary>
        /// Client-side flow
        /// </summary>
        [EnumMember]
        Implicit = 0,

        /// <summary>
        /// Server-side flow
        /// </summary>
        [EnumMember]
        Explicit = 1
    }
}
