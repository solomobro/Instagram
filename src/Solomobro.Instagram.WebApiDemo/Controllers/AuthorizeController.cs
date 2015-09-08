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
        
            return Ok(auth.AuthorizationUri);
        }

        [HttpGet]
        [Route("api/authorize")]
        public IHttpActionResult Authorize(HttpRequestMessage req)
        {
            try
            {
                var uri = req.RequestUri;
                var auth = GetInstagramAuthenticator();
                auth.AuthorizeAsync(uri).Wait();
                
                return Redirect("LoggedIn.html");
            }
            catch (Exception)
            {
                return Redirect("Failed.html");
            }
        }

        private OAuth GetInstagramAuthenticator()
        {
            return new OAuth(AuthSettings.InstaClientID, AuthSettings.InstaClientSecret, AuthSettings.InstaRedirectUrl);
        }
    }
}