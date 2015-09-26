using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class GetPostsResponse : Response, IPagination<GetPostsResponse>
    {
        internal GetPostsResponse() { }

        public IReadOnlyList<Post> Data => DataInternal?.AsReadOnly();
            
        [DataMember(Name = "data")]
        internal List<Post> DataInternal; 

        public async Task<GetPostsResponse> GetNextResultAsync()
        {
            throw new NotImplementedException();
        }
    }
}
