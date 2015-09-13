using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Solomobro.Instagram.WebApiDemo.Settings;

namespace Solomobro.Instagram.WebApiDemo.Controllers
{
    public class AuthorizeController : ApiController
    {
        [HttpGet]
        [Route("api/login")]
        public IHttpActionResult GetLoginUri(HttpRequestMessage req)
        {
            var auth = GetInstagramAuthenticator();
        
            return Ok(auth.AuthenticationUri);
        }

        [HttpGet]
        [Route("api/authorize")]
        public async Task<IHttpActionResult> AuthorizeAsync(HttpRequestMessage req)
        {
            var port = Properties.Settings.Default.ServerPort;
            try
            {
                var uri = req.RequestUri;
                var auth = GetInstagramAuthenticator();
                var result = await auth.ValidateAuthenticationAsync(uri);

                if (result.Success)
                {
                    return Redirect($"http://localhost:{port}/LoggedIn.html");
                }
                else
                {
                    return Redirect($"http://localhost:{port}/Failed.html");
                }
            }
            catch (Exception)
            {
                return Redirect($"http://localhost:{port}/Failed.html");
            }
        }

        private OAuth GetInstagramAuthenticator()
        {
            return new OAuth(AuthSettings.InstaClientID, AuthSettings.InstaClientSecret, AuthSettings.InstaRedirectUrl);
        }
    }
}