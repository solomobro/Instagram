using System;
using System.Collections.Generic;

namespace Solomobro.Instagram
{
    /// <summary>
    /// Internal-only homegrown IOC container.
    /// It's not really meant to make our code more flexible
    /// Just makes it possible to substitute some types for unit testing
    /// </summary>
    internal static class Ioc
    {
        private static readonly Dictionary<string, object> Substitutes = new Dictionary<string, object>();

        /// <summary>
        /// Meant to be called within a unit test only
        /// DO NOT CALL THIS INSIDE THE LIBRARY
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mockInstance">the mock instance</param>
        public static void Substitute<T>(T mockInstance) where T: class 
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("Not allowed to substitute a concrete type - must be an interface");
            }

            if (mockInstance == null)
            {
                throw new InvalidOperationException("Not allowed to substitute with a null instance");
            }

            Substitutes[Key<T>()] = mockInstance;
        }

        public static T Resolve<T>() where T: class 
        {
            object instance;
            if (Substitutes.TryGetValue(Key<T>(), out instance))
            {
                return (T) instance;
            }

            return null;
        }

        private static string Key<T>()
        {
            return (typeof(T)).FullName;
        }
    }
}
