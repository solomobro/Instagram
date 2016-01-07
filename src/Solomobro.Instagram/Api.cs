using Solomobro.Instagram.Endpoints;
using Solomobro.Instagram.Interfaces;

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

            var apiClient = Ioc.Resolve<IApiClient>() ?? new ApiClient();

            // initialize endpoints
            Users = new Users(apiClient, accessToken);
            Relationships = new Relationships(apiClient, accessToken);
            Media = new Media(apiClient, accessToken);
            Comments = new Comments(apiClient, accessToken);
            Likes = new Likes(apiClient, accessToken);
            Tags = new Tags(apiClient, accessToken);
            Locations = new Locations(apiClient, accessToken);
        }

        public Users Users { get; private set; }

        public Relationships Relationships { get; private set; }

        public Media Media { get; private set; }

        public Comments Comments { get; private set; }

        public Likes Likes { get; private set; }

        public Tags Tags { get; private set; }

        public Locations Locations { get; private set; }
    }
}
