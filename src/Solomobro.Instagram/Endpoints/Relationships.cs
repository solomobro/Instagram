using System;
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

        public async Task<CollectionResponse<User>> GetFollowedByAsync()
        {
            return  await _users.GetFollowedByAsync().ConfigureAwait(false);
        }

        public async Task<CollectionResponse<User>> GetRequestedByAsync()
        {
            return await _users.GetFollowedByAsync().ConfigureAwait(false);
        }

        public async Task<ObjectResponse<RelationShip>> GetRelationshipAsync(string userId)
        {
            return await _users.GetRelationshipAsync(userId).ConfigureAwait(false);
        }

        public async Task<ObjectResponse<RelationShip>> ModifyRelationshipAsync(string userId, string action)
        {
            return await _users.PostRelationshipAsync(userId, action).ConfigureAwait(false);
        }
    }
}
