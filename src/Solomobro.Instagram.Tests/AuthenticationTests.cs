using NUnit.Framework;
using Solomobro.Instagram.Authentication;

namespace Solomobro.Instagram.Tests
{
    [TestFixture]
    class AuthenticationTests
    {
        private const string ClientId = "CLIENT-ID";
        private const string ClientSecret = "CLIENT-SECRET";
        private const string RedirectUri = "REDIRECT-URI";
        private const string BaseExplicitUri = "https://api.instagram.com/oauth";
        private const string BaseImplicitUri = "https://instagram.com/oauth";

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
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri, AuthenticationMethod.Implicit);
            auth.AddScope(OAuthScope.Basic);
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));
        }

    }
}
