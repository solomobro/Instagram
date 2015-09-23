using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solomobro.Instagram.Authentication;
using Solomobro.Instagram.Interfaces;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Tests.Mocks
{
    class MockAccessTokenRetriever : IAccessTokenRetriever
    {
        public Task<ExplicitAuthResponse> Authenticate(Uri authEndpoint, IEnumerable<KeyValuePair<string, string>> authParams)
        {
            var resp = new ExplicitAuthResponse
            {
                AccessToken = "ACCESS-TOKEN",
                User = new User
                {
                    Id = "1234567890",
                    UserName = "solomobro",
                    FullName = "So Lo Mo Bro",
                    ProfilePicture = "http://foo.com/bar",
                    Website = "https://github.com/solomobro/Instagram",
                    Bio = "Making the world a better place",
                    Counts = new UserStats
                    {
                        Follow = 100,
                        FollowedBy = 200,
                        Media = 300
                    }
                }
            };

            return Task.FromResult(resp);
        }
    }
}
