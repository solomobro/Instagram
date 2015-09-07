namespace Solomobro.Instagram
{
    /// <summary>
    /// The entry point for interacting with the Instagram API
    /// </summary>
    public class Api
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _accessToken;

        internal Api(string clientId, string clientSecret, string accessToken)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _accessToken = accessToken;
        }
    }
}
