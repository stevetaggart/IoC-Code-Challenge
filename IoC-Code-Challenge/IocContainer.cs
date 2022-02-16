using System;
using System.Collections.Generic;

namespace IoC_Code_Challenge
{
    public class IocContainer
    {
        private readonly Dictionary<Type, Func<object>> registrations = new();

        public void Register<T1, T2>(LifeCycleType lifeCycle = LifeCycleType.Transient) where T2 : class, new()
        {
            if (lifeCycle == LifeCycleType.Singleton)
            {
                var lazy = new Lazy<T2>(Activator.CreateInstance<T2>());
                registrations.Add(typeof(T1), () => lazy.Value);
            }
            else
            {
                registrations.Add(typeof(T1), () => Activator.CreateInstance(typeof(T2)));
            }
        }

        public T Resolve<T>()
        {
            if (!registrations.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"{typeof(T)} has not been registered.");
            }
            return (T)registrations[typeof(T)]();
        }
    }
}