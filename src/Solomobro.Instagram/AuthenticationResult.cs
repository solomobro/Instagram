using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Entities;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Result of a call to one of the Authorize methods in <see cref="OAuth"/>
    /// </summary>
    public class AuthenticationResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }

        /// <summary>
        /// The Authenticated user's basic info. Available only with Server-side/Explicit authentication
        /// </summary>
        public User User { get; internal set; }
    }
}
