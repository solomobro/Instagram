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

        /// <summary>
        /// Implements GET /users/self/follows
        /// </summary>
        public async Task<CollectionResponse<User>> GetFollowsAsync()
        {
            return await _users.GetFollowsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Implements /users/self/followed-by
        /// </summary>
        public async Task<CollectionResponse<User>> GetFollowedByAsync()
        {
            return  await _users.GetFollowedByAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/self/requested-by
        /// </summary>
        public async Task<CollectionResponse<User>> GetRequestedByAsync()
        {
            return await _users.GetFollowedByAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Implements GET /users/{user-id}/relationship
        /// </summary>
        /// <param name="userId"></param>
        public async Task<Response<RelationShip>> GetRelationshipAsync(string userId)
        {
            return await _users.GetRelationshipAsync(userId).ConfigureAwait(false);
        }

        /// <summary>
        /// Implements  POST /users/{user-id}/relationship
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <param name="action">either one of: follow|unfollow|ignore|approve</param>
        /// <returns></returns>
        public async Task<Response<RelationShip>> ModifyRelationshipAsync(string userId, string action)
        {
            return await _users.PostRelationshipAsync(userId, action).ConfigureAwait(false);
        }
    }
}
