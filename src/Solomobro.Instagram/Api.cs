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
            var media = new Media(epBase, accessToken);

            this.Users = users;
            this.Relationships = new Relationships(users);
            this.Media = media;
        }

        public Users Users { get; private set; }

        public Relationships Relationships { get; private set; }

        public Media Media { get; private set; }
    }
}
