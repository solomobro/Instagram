using System;
using NUnit.Framework;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Exceptions;
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
            var auth = GetExplicitAuth();
            Assert.That(auth.ClientId, Is.EqualTo(ClientId));
            Assert.That(auth.ClientSecret, Is.EqualTo(ClientSecret));
            Assert.That(auth.RedirectUri, Is.EqualTo(RedirectUri));
        }

        [Test]
        public void DefaultExplicitUriIsValid()
        {
            var auth = GetExplicitAuth();
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));
        }

        [Test]
        public void DefaultImplicitUriIsValid()
        {
            var auth = GetImplicitAuth();
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));
        }

        [Test]
        public void AddingScopesToExplicitAuthWorks()
        {
            // test that adding basic scope doesn't screw up the scope list
            var basicScope = new[] {AccessScope.Basic};
            var basicAuth = new ExplicitAuth(ClientId, ClientSecret, RedirectUri, basicScope);
            var uri = basicAuth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            var bunchOfScopes = new[]
            {AccessScope.Basic, AccessScope.Relationships, AccessScope.Comments, AccessScope.Likes, AccessScope.Comments};
            var complexAuth = new ExplicitAuth(ClientId, ClientSecret, RedirectUri, bunchOfScopes) ;
            uri = complexAuth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void AddingScopesToImplicitAuthWorks()
        {
            // test that adding basic scope doesn't screw up the scope list
            var basicScope = new[] { AccessScope.Basic };
            var basicAuth = new ImplicitAuth(ClientId, ClientSecret, RedirectUri, basicScope);
            var uri = basicAuth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            var bunchOfScopes = new[]
            {AccessScope.Basic, AccessScope.Relationships, AccessScope.Comments, AccessScope.Likes, AccessScope.Comments};
            var complexAuth = new ImplicitAuth(ClientId, ClientSecret, RedirectUri, bunchOfScopes);
            uri = complexAuth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseImplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=token&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void CanAuthenticateWithAccessToken()
        {
            var auth = GetExplicitAuth();
            Assert.That(auth.IsAuthenticated, Is.False);

            auth.AuthenticateWithAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void VerifyAccessTokenCannotBeChanged()
        {
            var auth = GetExplicitAuth();
            auth.AuthenticateWithAccessToken(AccessToken);

            Assert.That(auth.IsAuthenticated);
            Assert.That(() => auth.AuthenticateWithAccessToken("FAKE-ACCESS-TOKEN"), Throws.InstanceOf<AlreadyAuthenticatedException>());
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void CanAuthenticateExplictily()
        {
            Ioc.Substitute<IAccessTokenRetriever>(new MockAccessTokenRetriever());

            var instagramredirect = $"{RedirectUri}?code={AccessCode}";
            var auth = GetExplicitAuth();

            var result = auth.AuthenticateFromResponseAsync(new Uri(instagramredirect)).Result;
            Assert.That(result.Success);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void CanAuthenticateImplicitly()
        {
            var instagramRedirect = $"{RedirectUri}#access_token={AccessToken}";
            var auth = GetImplicitAuth();

            var result = auth.AuthenticateFromResponse(new Uri(instagramRedirect));
            Assert.That(result.Success);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void BadUriFailsAuthentication()
        {
            var auth = GetExplicitAuth();
            var badUri =
                $"{RedirectUri}?error=access_denied&error_reason=user_denied&error_description=The+user+denied+your+request";
            var result = auth.AuthenticateFromResponseAsync(new Uri(badUri)).Result;
            Assert.That(!result.Success);
        }

        private ExplicitAuth GetExplicitAuth()
        {
            return new ExplicitAuth(ClientId, ClientSecret, RedirectUri);
        }

        private ImplicitAuth GetImplicitAuth()
        {
            return new ImplicitAuth(ClientId, ClientSecret, RedirectUri);
        }

    }
}
