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
            try
            {
                var uri = req.RequestUri;
                var auth = GetInstagramAuthenticator();
                var result = await auth.ValidateAuthenticationAsync(uri);

                if (result.Success)
                {
                    return Redirect("http://localhost:8012/LoggedIn.html");
                }
                else
                {
                    return Redirect("http://localhost:8012/Failed.html");
                }
            }
            catch (Exception)
            {
                return Redirect("http://localhost:8012/Failed.html");
            }
        }

        private OAuth GetInstagramAuthenticator()
        {
            return new OAuth(AuthSettings.InstaClientID, AuthSettings.InstaClientSecret, AuthSettings.InstaRedirectUrl, AuthenticationMethod.Implicit);
        }
    }
}