using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

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

        private static DataContractJsonSerializer GetSerializerForType<T>()
        {
            var t = typeof (T);
            return Serializers.GetOrAdd(t, type => new DataContractJsonSerializer(type));
        }
    }
}
