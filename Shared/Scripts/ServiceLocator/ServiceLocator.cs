using System;
using System.Collections.Generic;
using TimiShared.Debug;

namespace TimiShared.Service {
    public static class ServiceLocator {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void RegisterService<T>(IService service) {
            _services[typeof(T)] = service;
        }

        public static T Service<T>() where T : class, new() {
            IService service = null;
            if (_services.TryGetValue(typeof(T), out service)) {
                return service as T;
            }
            TimiDebug.LogWarningColor("No service registered for type " + typeof(T).Name, LogColor.red);
            service = new T() as IService;
            ServiceLocator.RegisterService<T>(service);
            return service as T;
        }
    }
}