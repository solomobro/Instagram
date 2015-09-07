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
using Solomobro.Instagram.WebApiDemo.Settings;

namespace Solomobro.Instagram.WebApiDemo.Controllers
{
    public class AuthorizeController : ApiController
    {
        [HttpGet]
        [Route("api/authorize")]
        public IHttpActionResult AuthorizeInstagram(HttpRequestMessage req)
        {
            var clientId = Settings.Settings.InstaClientID;
            var clientSecret = Settings.Settings.InstaClientSecret;
            var redirectUrl = Settings.Settings.InstaRedirectUrl;
            var authConfig = new OAuthConfig(clientId, clientSecret, redirectUrl);

            using (var ctFactory = new CancellationTokenSource())
            {
                var ct = ctFactory.Token;
                var api = new Api(new Redirector(req, ct), authConfig);
                api.AuthorizeAsync().Wait((int)TimeSpan.FromMinutes(1).TotalMilliseconds, ct);
                
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
            public Task<HttpResponseMessage> ProcessAuthorizationAsync(Uri uri)
            {
                
                var resp = new RedirectResult(uri, _request);
                var foo =  resp.ExecuteAsync(_token);
                return foo;
            }
        }
    }
}