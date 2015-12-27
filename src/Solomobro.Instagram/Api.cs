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

        internal Api(string clientId, string clientSecret, string accessToken)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _accessToken = accessToken;

            // initialize endpoints
            var epBase = new EndpointBase(accessToken);
            var users = new Users(epBase, accessToken);
            var media = new Media(epBase, accessToken);
            var comments = new Comments(media);
            var likes = new Likes(media);
            var tags = new Tags(epBase, accessToken);
            var locations = new Locations(epBase, accessToken);

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
