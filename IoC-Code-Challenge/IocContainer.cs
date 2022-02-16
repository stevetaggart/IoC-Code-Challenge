using System;
using System.Collections.Generic;
using System.Linq;

namespace IoC_Code_Challenge
{
    public class IocContainer
    {
        private readonly Dictionary<Type, Func<object>> registrations = new();

        public void Register<T1, T2>(LifeCycleType lifeCycle = LifeCycleType.Transient) where T2 : class, new()
        {
            if (lifeCycle == LifeCycleType.Singleton)
            {
                // Create a lazy object initialized with an instance of T2
                var lazy = new Lazy<T2>((T2)GetInstance(typeof(T2)));
                // Always return the same object
                registrations.Add(typeof(T1), () => lazy.Value);
            }
            else
            {
                registrations.Add(typeof(T1), () => GetInstance(typeof(T2)));
            }
        }

        public T Resolve<T>()
        {
            return (T)GetInstance(typeof(T));
        }

        private object GetInstance(Type type)
        {
            if (registrations.TryGetValue(type, out Func<object> fac))
            {
                return fac();
            }
            else if (!type.IsAbstract)
            { 
                return CreateInstance(type); 
            }
            throw new InvalidOperationException("No registration found for " + type);
        }

        private object CreateInstance(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();
            var paramTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = paramTypes.Select(GetInstance).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }
    }
}