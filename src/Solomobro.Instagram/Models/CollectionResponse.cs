using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class CollectionResponse<T> : IResponse, IRateLimitable, IEnumerable
    {
        internal CollectionResponse() { }

        public int Count => Data?.Count ?? 0;

        [DataMember(Name = "meta")]
        public Meta Meta { get; internal set; }

        public RateLimit RateLimit { get; internal set; }

        public IReadOnlyList<T> Data => DataInternal.AsReadOnly();

        [DataMember(Name = "data")]
        internal List<T> DataInternal;

        [DataMember(Name = "pagination")]
        internal Pagination Pagination { get; set; }

        /// <summary>
        /// Implements pagination
        /// </summary>
        /// <returns>The next result set if any, else null</returns>
        public async Task<CollectionResponse<T>> GetNextResultAsync()
        {
            if (string.IsNullOrWhiteSpace(Pagination?.NextUrlInternal))
            {
                return null;
            }

            var apiClient = Ioc.Resolve<IApiClient>() ?? new ApiClient();
            return await apiClient.GetCollectionResponseAsync<T>(Pagination.NextUrl).ConfigureAwait(false);
        }

        void IRateLimitable.SetRateLimit(RateLimit limit)
        {
            RateLimit = limit;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Data == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return Data.GetEnumerator();
        }
    }
}
