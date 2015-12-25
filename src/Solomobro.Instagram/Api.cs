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

            // initialize endpoints
            var epBase = new EndpointBase(accessToken);
            var users = new Users(epBase, accessToken);
            Users = users;
            Relationships = new Relationships(users);
        }

        public Users Users { get; private set; }

        public Relationships Relationships { get; private set; }
    }
}
