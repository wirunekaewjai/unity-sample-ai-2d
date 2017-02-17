using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Observe : Attribute
    {
        public readonly object eventID;

        public Observe()
        {
            this.eventID = null;
        }

        public Observe(object eventID)
        {
            this.eventID = eventID;
        }

        public static Dictionary<object, MethodInfo> FindObserveMethods(object target)
        {
            var observes = new Dictionary<object, MethodInfo>();

            var type = target.GetType();
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

            return observes;
        }
    }
}
