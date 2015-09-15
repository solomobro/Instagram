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
            var auth = GetDefaultOAuth();
            Assert.That(auth.ClientId, Is.EqualTo(ClientId));
            Assert.That(auth.ClientSecret, Is.EqualTo(ClientSecret));
            Assert.That(auth.RedirectUri, Is.EqualTo(RedirectUri));
        }


        [Test]
        public void DefaultAuthMethodIsExplicit()
        {
            var auth = GetDefaultOAuth();
            Assert.That(auth.AuthMethod, Is.EqualTo(AuthenticationMethod.Explicit));
        }

        [Test]
        public void DefaultExplicitUriIsValid()
        {
            var auth = GetDefaultOAuth();
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
            var auth = GetDefaultOAuth();
            auth.AddScope(OAuthScope.Basic);
            var uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic"));

            // test adding scopes other than basic, and they can be repeated
            auth = GetDefaultOAuth();
            auth.AddScope(OAuthScope.Basic, OAuthScope.Relationships, OAuthScope.Comments, OAuthScope.Likes, OAuthScope.Comments);
            uri = auth.AuthenticationUri;
            Assert.That(uri.OriginalString, Is.EqualTo($"{BaseExplicitUri}/authorize/?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=basic+comments+likes+relationships"));
        }

        [Test]
        public void CanAuthenticateWithAccessToken()
        {
            var auth = GetDefaultOAuth();
            Assert.That(auth.IsAuthenticated, Is.False);

            auth.AuthenticateWithAccessToken(AccessToken);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void VerifyAccessTokenCannotBeChanged()
        {
            var auth = GetDefaultOAuth();
            auth.AuthenticateWithAccessToken(AccessToken);

            Assert.That(auth.IsAuthenticated);
            Assert.That(() => auth.AuthenticateWithAccessToken("FAKE-ACCESS-TOKEN"), Throws.InstanceOf<AlreadyAuthenticatedException>());
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void CanAuthenticateExplictily()
        {
            Ioc.Substitute<IExplicitAuthenticator>(new MockExplicitAuthenticator());

            var instagramredirect = $"{RedirectUri}?code={AccessCode}";
            var auth = GetDefaultOAuth();

            var result = auth.ValidateAuthenticationAsync(new Uri(instagramredirect)).Result;
            Assert.That(result.Success);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void CanAuthenticateImplicitly()
        {
            var instagramRedirect = $"{RedirectUri}#access_token={AccessToken}";
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri, AuthenticationMethod.Implicit);

            var result = auth.ValidateAuthenticationAsync(instagramRedirect).Result;
            Assert.That(result.Success);
            Assert.That(auth.IsAuthenticated);
        }

        [Test]
        public void BadUriFailsAuthentication()
        {
            var auth = GetDefaultOAuth();
            var badUri =
                $"{RedirectUri}?error=access_denied&error_reason=user_denied&error_description=The+user+denied+your+request";
            var result = auth.ValidateAuthenticationAsync(badUri).Result;
            Assert.That(!result.Success);
        }

        private OAuth GetDefaultOAuth()
        {
            return new OAuth(ClientId, ClientSecret, RedirectUri);
        }

    }
}
