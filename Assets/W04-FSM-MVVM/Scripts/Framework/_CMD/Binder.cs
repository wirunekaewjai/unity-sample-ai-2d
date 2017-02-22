using System;
using System.Reflection;
using System.Collections.Generic;

namespace Wirune.W04
{
    public static class Binder
    {
        private static Dictionary<Type, Dictionary<object, MethodInfo>> s_Caches = new Dictionary<Type, Dictionary<object, MethodInfo>>();

        private static Dictionary<object, MethodInfo> GetBindMethods(object target)
        {
            var type = target.GetType();

            if (s_Caches.ContainsKey(type))
            {
                return s_Caches[type];
            }

            var map = new Dictionary<object, MethodInfo>();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var attributeType = typeof(Bind);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(attributeType, false);
                if (attributes.Length > 0)
                {
                    var attribute = (Bind) attributes[0];
                    var command = (null != attribute.command) ? attribute.command : method.Name;

                    map.Add(command, method);
                }
            }

            s_Caches.Add(type, map);
            return map;
        }

        public static void Invoke(object target, object command, params object[] parameters)
        {
            var methodInfos = GetBindMethods(target);

            if (methodInfos.ContainsKey(command))
            {
                methodInfos[command].Invoke(target, parameters);
            }
        }
    }
}
