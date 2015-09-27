using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Solomobro.Instagram.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage message)
        {
            using (var data = await message.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var sr = new StreamReader(data))
            using (var jr = new JsonTextReader(sr))
            {
                return Serializer.Deserialize<T>(jr);
            }
        }
    }
}
