﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Endpoints
{
    public class Relationships
    {
        private Users _users;

        internal Relationships(Users usersEndpoint)
        {
            _users = usersEndpoint;
        }

        public async Task<CollectionResponse<User>> GetFollowsAsync()
        {
            return await _users.GetFollowsAsync().ConfigureAwait(false);
        } 
    }
}
