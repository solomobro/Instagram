using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    internal class JsonSerializer
    {
        private readonly ConcurrentDictionary<Type, DataContractJsonSerializer> _serializers;

        internal JsonSerializer()
        {
            _serializers = new ConcurrentDictionary<Type, DataContractJsonSerializer>();
        }

        public T Deserialize<T>(Stream stream)
        {
            var serializer = GetSerializerForType<T>();
            return (T) serializer.ReadObject(stream);
        } 

        private DataContractJsonSerializer GetSerializerForType<T>()
        {
            return _serializers.GetOrAdd(typeof (T), type => new DataContractJsonSerializer(type));
        }
    }
}
