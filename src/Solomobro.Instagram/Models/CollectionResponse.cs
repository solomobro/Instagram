using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Solomobro.Instagram.Endpoints;
using Solomobro.Instagram.Interfaces;

namespace Solomobro.Instagram.Models
{
    [DataContract]
    public class CollectionResponse<T> : IResponse, IEnumerable
    {
        /// <summary>
        /// We need this to be lazy because we don't want to risk instantiating
        /// an http client unless we absolutely need to
        /// </summary>
        private readonly Lazy<IApiClient> _lazyApiClient;


        internal CollectionResponse()
        {
            _lazyApiClient = new Lazy<IApiClient>(
                () => Ioc.Resolve<IApiClient>() ?? new ApiClient());
        }

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
            if (Pagination == null || string.IsNullOrWhiteSpace(Pagination.NextUrlInternal))
            {
                return null;
            }

            var apiClient = _lazyApiClient.Value;
            var apiReply = await apiClient.GetAsync<CollectionResponse<T>>(Pagination.NextUrl).ConfigureAwait(false);
            var resp = apiReply.Data;
            resp.RateLimit = apiReply.RateLimit;
            return resp;
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
