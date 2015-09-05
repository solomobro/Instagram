namespace Solomobro.Instagram
{
    /// <summary>
    /// Authentication methods supported by the Instagram API: implicit (client) or explicit (server)
    /// </summary>
    public enum AuthenticationMethod
    {
        /// <summary>
        /// Client-side flow
        /// </summary>
        Implicit = 0,

        /// <summary>
        /// Server-side flow
        /// </summary>
        Explicit = 1
    }
}
