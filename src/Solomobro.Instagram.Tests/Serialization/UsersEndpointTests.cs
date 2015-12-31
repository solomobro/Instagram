using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Tests.Serialization
{
    [TestFixture]
    class UsersEndpointTests
    {
        private static readonly  JsonSerializer Serializer = new JsonSerializer();

        [Test]
        public void CanDeserializeGetUser()
        {
            const string response =
                "{\"meta\":{\"code\":200},\"data\":{\"username\":\"solomobro\",\"bio\":\"This is a developer test account for an Instagram .NET SDK\",\"website\":\"https://github.com/solomobro/Instagram\",\"profile_picture\":\"https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-19/s150x150/11917846_398932440308986_1930181477_a.jpg\",\"full_name\":\"So Lo Mo, Bro!\",\"counts\":{\"media\":2,\"followed_by\":3,\"follows\":1},\"id\":\"12345678\"}}";

            using (var s = new MemoryStream(Encoding.ASCII.GetBytes(response)))
            {
                var resp = Serializer.Deserialize<Response<User>>(s);

                Assert.That(resp, Is.Not.Null);
            }

        }
    }
}
