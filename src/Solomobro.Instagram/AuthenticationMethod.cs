namespace Solomobro.Instagram
{
    /// <summary>
    /// Authentication methods supported by the Instagram API: implicit (client) or explicit (server)
    /// </summary>
    public enum AuthenticationMethod
    {
        /// <summary>
        /// Server-side flow
        /// </summary>
        Explicit = 0,

        /// <summary>
        /// Client-side flow
        /// </summary>
        Implicit = 1,
    }
}
