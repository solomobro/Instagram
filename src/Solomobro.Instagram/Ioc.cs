using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Internal-only homegrown IOC container.
    /// It's not really meant to make our code more flexible
    /// Just makes it possible to substitute some types for unit testing
    /// </summary>
    internal static class Ioc
    {
        private static Dictionary<string, object> _substitutes = new Dictionary<string, object>();

        /// <summary>
        /// Meant to be called within a unit test only
        /// DO NOT CALL THIS INSIDE THE LIBRARY
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mockInstance">the mock instance</param>
        public static void Substitute<T>(T mockInstance)
        {
            _substitutes[Key<T>()] = mockInstance;
        }

        public static T Resolve<T>() where T: class 
        {
            object instance;
            if (_substitutes.TryGetValue(Key<T>(), out instance))
            {
                return (T) instance;
            }

            return null;
        }

        private static string Key<T>()
        {
            return typeof (T).FullName;
        }
    }
}
