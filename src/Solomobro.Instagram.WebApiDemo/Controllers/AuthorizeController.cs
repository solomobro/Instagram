using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.WebApiDemo.Controllers
{
    public class AuthorizeController : ApiController
    {
        [HttpGet]
        [Route("api/authorize")]
        public IHttpActionResult AuthorizeInstagram(HttpRequestMessage req)
        {
            var clientId = "af35f1d85c684c2da93e1d5a2c55550a";
            var clientSecret = "e0c5a465e3274519b0544c3dfd54ac5b";
            var redirectUrl = "http://localhost";
            var authConfig = new OAuthConfig(clientId, clientSecret, redirectUrl);

            using (var ctFactory = new CancellationTokenSource())
            {
                var ct = ctFactory.Token;
                var api = new Api(new Redirector(req, ct), authConfig);
                api.AuthorizeAsync();
                return Ok(api.AuthUri);
            }




            return Ok("");
        }

        private class Redirector : IInteractiveAuthorizationProvider
        {
            private readonly HttpRequestMessage _request;
            private readonly CancellationToken _token;

            public Redirector(HttpRequestMessage req, CancellationToken token)
            {
                _request = req;
                _token = token;
            }
            public async Task<HttpResponseMessage> ProcessAuthorizationAsync(Uri uri)
            {
                // send user to instagram login/auth window
                return await FOO();
                
            }

            private async Task<HttpResponseMessage> FOO()
            {
                throw new NotImplementedException();
            }
        }
    }
}