using System;
using System.Reflection;
using System.Collections.Generic;

namespace Wirune.W03
{
    public static class ObserveUtil
    {
        private static Dictionary<Type, Dictionary<object, MethodInfo>> s_Caches = new Dictionary<Type, Dictionary<object, MethodInfo>>();

        private static Dictionary<object, MethodInfo> GetObserveMethods(object target)
        {
            var type = target.GetType();

            if (s_Caches.ContainsKey(type))
            {
                return s_Caches[type];
            }

            var observes = new Dictionary<object, MethodInfo>();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var attributeType = typeof(Observe);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(attributeType, false);
                if (attributes.Length > 0)
                {
                    var attribute = (Observe)attributes[0];
                    var eventID = (null != attribute.eventID) ? attribute.eventID : method.Name;

                    observes.Add(eventID, method);
                }
            }

            s_Caches.Add(type, observes);
            return observes;
        }

        public static void Invoke(object target, object eventID, params object[] parameters)
        {
            var methodInfos = GetObserveMethods(target);

            if (methodInfos.ContainsKey(eventID))
            {
                methodInfos[eventID].Invoke(target, parameters);
            }
        }
    }
}
