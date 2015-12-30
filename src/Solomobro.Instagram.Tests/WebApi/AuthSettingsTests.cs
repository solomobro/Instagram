using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Solomobro.Instagram.WebApiDemo.Settings;

namespace Solomobro.Instagram.Tests.WebApi
{
    [TestFixture]
    public class AuthSettingsTests
    {
        const string Settings = @"#Instagram Settings
InstaWebsiteUrl=https://github.com/solomobro/Instagram
InstaClientId=<CLIENT-ID>
InstaClientSecret=<CLIENT-SECRET>
InstaRedirectUrl=http://localhost:56841/api/authorize";

        [Test]
        public void AllAuthSettingsPropertiesAreSet()
        {
            Assert.That(AuthSettings.InstaClientId, Is.Null);
            Assert.That(AuthSettings.InstaClientSecret, Is.Null);
            Assert.That(AuthSettings.InstaRedirectUrl, Is.Null);
            Assert.That(AuthSettings.InstaWebsiteUrl, Is.Null);

            using (var memStream = new MemoryStream(Encoding.ASCII.GetBytes(Settings)))
            {
                AuthSettings.LoadSettings(memStream);
            }

            Assert.That(AuthSettings.InstaClientId, Is.Not.Null);
            Assert.That(AuthSettings.InstaClientSecret, Is.Not.Null);
            Assert.That(AuthSettings.InstaRedirectUrl, Is.Not.Null);
            Assert.That(AuthSettings.InstaWebsiteUrl, Is.Not.Null);
        }
    }
}
