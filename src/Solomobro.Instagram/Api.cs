using Solomobro.Instagram.Endpoints;

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

        internal const string Self = "self";


        internal Api(string clientId, string clientSecret, string accessToken)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _accessToken = accessToken;

            var epBase = new EndpointBase(accessToken);
            Users = new Users(epBase, accessToken);
        }

        public Users Users { get; private set; }
    }
}
