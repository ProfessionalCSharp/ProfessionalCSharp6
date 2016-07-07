using System;
using System.Collections.Concurrent;

namespace Framework
{
    public class ServiceLocator
    {
        private ServiceLocator()
        {

        }

        public static ServiceLocator Instance => new ServiceLocator();

        private static ConcurrentDictionary<Type, object> s_types = new ConcurrentDictionary<Type, object>();
        public bool Register(Type serviceType, object instance) =>
            s_types.TryAdd(serviceType, instance);

        public bool Register(Type serviceType) =>
            s_types.TryAdd(serviceType, null);

        public bool Register<T>(Type serviceType, Func<T> factory) =>
            s_types.TryAdd(serviceType, factory);

        public T Resolve<T>()
            where T : class, new()
        {
            object val;
            if (s_types.TryGetValue(typeof(T), out val))
            {
                if (val == null)
                {
                    Type t = typeof(T).GetGenericTypeDefinition();
                }
                else if (val is Func<T>)
                {
                    return ((Func<T>)val)();  // invoke factory
                }
                else
                {
                    return val as T;
                }
            }
            return null;
        }

    }
}
