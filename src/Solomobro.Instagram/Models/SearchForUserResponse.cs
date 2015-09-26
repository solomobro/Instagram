using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class SearchForUserResponse : Response, IPagination<SearchForUserResponse>
    {
        internal SearchForUserResponse() { }

        public IReadOnlyList<UserSearchResult> Data => DataInternal?.AsReadOnly(); 
            
        [DataMember(Name = "data")]
        internal List<UserSearchResult> DataInternal; 

        public Task<SearchForUserResponse> GetNextResultAsync()
        {
            throw new NotImplementedException();
        }
    }
}
