using NUnit.Framework;
using Solomobro.Instagram.Authentication;

namespace Solomobro.Instagram.Tests
{
    [TestFixture]
    class AuthenticationTests
    {
        const string ClientId = "CLIENT-ID";
        const string ClientSecret = "CLIENT-SECRET";
        const string RedirectUri = "REDIRECT-URI";

        [Test]
        public void DefaultAuthMethodIsExplicit()
        {
            var auth = new OAuth(ClientId, ClientSecret, RedirectUri);

            Assert.That(auth.AuthMethod, Is.EqualTo(AuthenticationMethod.Explicit));
        }

    }
}
