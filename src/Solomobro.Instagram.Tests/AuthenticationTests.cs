using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Tests.Mocks;

namespace Solomobro.Instagram.Tests
{
    [TestFixture]
    class AuthenticationTests
    {
        private const string ClientId = "CLIENT-ID";
        private const string ClientSecret = "CLIENT-SECRET";
        private const string RedirectUri = "http://REDIRECT-URI";
        private const string AccessCode = "ACCESS-CODE";
        private const string AccessToken = "ACCESS-TOKEN";
        private const string BaseExplicitUri = "https://api.instagram.com/oauth";
        private const string BaseImplicitUri = "https://instagram.com/oauth";

        [Test]
        public void ConfigSettingsAreCorrect()
        {
            var auth = GetOAuth();
            Assert.That(auth.ClientId, Is.EqualTo(ClientId));
            Assert.That(auth.ClientSecret, Is.EqualTo(ClientSecret));
            Assert.That(auth.RedirectUri, Is.EqualTo(RedirectUri));
        }

        [Test]
        public void DefaultExplicitUriIsValid()
        {
            var auth = GetOAuth();
            var uri = auth.ExplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));
        }

        [Test]
        public void DefaultImplicitUriIsValid()
        {
            var auth = GetOAuth();
            var uri = auth.ImplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));
        }

        [Test]
        public void AddingScopesWorksWithExplicitAuth()
        {
            // test that adding basic scope doesn't screw up the scope list
            var basicScope = new[] {Scopes.Basic};
            var basicAuth = new OAuth(ClientId, ClientSecret, RedirectUri, basicScope);
            var uri = basicAuth.ExplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            var bunchOfScopes = new[]
            {Scopes.Basic, Scopes.Relationships, Scopes.Comments, Scopes.Likes, Scopes.Comments};
            var complexAuth = new OAuth(ClientId, ClientSecret, RedirectUri, bunchOfScopes) ;
            uri = complexAuth.ExplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void AddingScopesWorksWithImplicitAuth()
        {
            // test that adding basic scope doesn't screw up the scope list
            var basicScope = new[] { Scopes.Basic };
            var basicAuth = new OAuth(ClientId, ClientSecret, RedirectUri, basicScope);
            var uri = basicAuth.ImplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            var bunchOfScopes = new[]
            {Scopes.Basic, Scopes.Relationships, Scopes.Comments, Scopes.Likes, Scopes.Comments};
            var complexAuth = new OAuth(ClientId, ClientSecret, RedirectUri, bunchOfScopes);
            uri = complexAuth.ImplicitAuthUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void CanAuthenticateWithAccessToken()
        {
            var auth = GetOAuth();
            Assert.That(auth.IsAuthenticated, Is.False);

            auth.AuthenticateFromAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void VerifyAccessTokenCannotBeChanged()
        {
            var auth = GetOAuth();
            auth.AuthenticateFromAccessToken(AccessToken);

            Assert.That(auth.IsAuthenticated);
            Assert.That(() => auth.AuthenticateFromAccessToken("FAKE-ACCESS-TOKEN"), Throws.InvalidOperationException);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public async Task CanAuthenticateExplictily()
        {
            Ioc.Substitute<IExplicitAuthenticator>(new MockExplicitAuthenticator());

            var instagramredirect = $"{RedirectUri}?code={AccessCode}";
            var auth = GetOAuth();

            var result = await auth.AuthenticateExplicitlyAsync(new Uri(instagramredirect));
            Assert.That(result.Success);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void BadUriFailsAuthentication()
        {
            var auth = GetOAuth();
            var badUri =
                $"{RedirectUri}?error=access_denied&error_reason=user_denied&error_description=The+user+denied+your+request";
            Assert.That( async () => await auth.AuthenticateExplicitlyAsync(new Uri(badUri)),
                Throws.InstanceOf<OAuthException>());
        }

        [Test]
        public void CannotCreateApiIfUnauthenticated()
        {
            var auth = GetOAuth();
            Assert.That(!auth.IsAuthenticated);
            Assert.Throws<InvalidOperationException>(() => auth.CreateApi());
        }

        [Test]
        public void CanCreateApiWhenAuthenticated()
        {
            var auth = GetOAuth();
            Assert.That(!auth.IsAuthenticated);
            auth.AuthenticateFromAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated);
            Assert.DoesNotThrow(() => auth.CreateApi());
        }

        private OAuth GetOAuth()
        {
            return new OAuth(ClientId, ClientSecret, RedirectUri);
        }

    }
}
