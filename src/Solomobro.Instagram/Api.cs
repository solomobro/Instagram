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

            // initialize endpoints
            var apiClient = Ioc.Resolve<IApiClient>() ?? new ApiClient();
            var users = new Users(apiClient, accessToken);
            var media = new Media(apiClient, accessToken);
            var comments = new Comments(apiClient, accessToken);
            var likes = new Likes(apiClient, accessToken);
            var tags = new Tags(apiClient, accessToken);
            var locations = new Locations(apiClient, accessToken);

            this.Users = users;
            this.Relationships = new Relationships(users);
            this.Media = media;
            this.Comments = comments;
            this.Likes = likes;
            this.Tags = tags;
            this.Locations = locations;
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
