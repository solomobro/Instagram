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
        [Route("api/login-uri")]
        public IHttpActionResult GetLoginUri(HttpRequestMessage req)
        {
            var auth = new OAuth("", "", "");
        
            return Ok(auth.AuthorizationUri);
        }

        [Route("api/authorize")]
        public IHttpActionResult Authorize(HttpRequestMessage req)
        {

            return Ok("authorized");
        }
    }
}