using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Solomobro.Instagram.Models;

namespace Solomobro.Instagram.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        private static readonly ConcurrentDictionary<Type, DataContractJsonSerializer> Serializers = new ConcurrentDictionary<Type, DataContractJsonSerializer>(); 

        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage message)
        {
            using (var data = await message.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                var serializer = GetSerializerForType<T>();
                return (T)serializer.ReadObject(data);
            }
        }

        private static int GetRateLimitMax(this HttpResponseMessage message)
        {
            IEnumerable<string> values;
            if (message.Headers.TryGetValues("X-Ratelimit-Limit", out values))
            {
                return int.Parse(values.First());
            }

            return -1;
        }

        private static int GetRateLimitRemaining(this HttpResponseMessage message)
        {
            IEnumerable<string> values;
            if (message.Headers.TryGetValues("X-Ratelimit-Remaining", out values))
            {
                return int.Parse(values.First());
            }

            return -1;
        }

        public static RateLimit GetRateLimitInfo(this HttpResponseMessage message)
        {
            return new RateLimit
            {
                Max = message.GetRateLimitMax(),
                Remaining = message.GetRateLimitRemaining()
            };
        }

        private static DataContractJsonSerializer GetSerializerForType<T>()
        {
            var t = typeof (T);
            return Serializers.GetOrAdd(t, type => new DataContractJsonSerializer(type));
        }
    }
}
