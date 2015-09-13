using NUnit.Framework;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Exceptions;

namespace Solomobro.Instagram.Tests
{
    [TestFixture]
    class AuthenticationTests
    {
        private const string ClientId = "CLIENT-ID";
        private const string ClientSecret = "CLIENT-SECRET";
        private const string RedirectUri = "REDIRECT-URI";
        private const string AccessCode = "ACCESS-CODE";
        private const string AccessToken = "ACCESS-TOKEN";
        private const string BaseExplicitUri = "https://api.instagram.com/oauth";
        private const string BaseImplicitUri = "https://instagram.com/oauth";

        [Test]
        public void ConfigSettingsAreCorrect()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            Assert.That(auth.ClientId, Is.EqualTo(ClientId));
            Assert.That(auth.ClientSecret, Is.EqualTo(ClientSecret));
            Assert.That(auth.RedirectUri, Is.EqualTo(RedirectUri));
        }


        [Test]
        public void DefaultAuthMethodIsExplicit()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            Assert.That(auth.AuthMethod, Is.EqualTo(AuthenticationMethod.Explicit));
        }

        [Test]
        public void DefaultExplicitUriIsValid()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));
        }

        [Test]
        public void DefaultImplicitUriIsValid()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri, AuthenticationMethod.Implicit);
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));
        }

        [Test]
        public void AddingScopesWorks()
        {
            // test that adding basic scope doesn't screw up the scope list
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            auth.AddScope(OAuthScope.Basic);
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            auth.AddScope(OAuthScope.Basic, OAuthScope.Relationships, OAuthScope.Comments, OAuthScope.Likes, OAuthScope.Comments);
            uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void CanAuthenticateWithAccessToken()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            Assert.That(auth.IsAuthenticated, Is.False);

            auth.AuthenticateWithAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated, Is.True);
        }

        [Test]
        public void VeryfyAccessTokenCannotBeChanged()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);
            Assert.That(auth.IsAuthenticated, Is.False);

            auth.AuthenticateWithAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated, Is.True);

            Assert.That(() => auth.AuthenticateWithAccessToken(AccessToken), Throws.InstanceOf<AlreadyAuthorizedException>());
        }

    }
}
