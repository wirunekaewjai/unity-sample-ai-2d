using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public class Command : ICommand
    {
        private object m_Target;
        private readonly Dictionary<object, MethodInfo> m_Methods = new Dictionary<object, MethodInfo>();

        public void BindCallbackAttribute(object target)
        {
            m_Target = target;
            m_Methods.Clear();

            var type = target.GetType();
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var attributeType = typeof(CommandCallback);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(attributeType, false);
                if (attributes.Length > 0)
                {
                    var attribute = (CommandCallback)attributes[0];
                    var command = (null != attribute.command) ? attribute.command : method.Name;

                    m_Methods.Add(command, method);
                }
            }
        }

        public void OnExecute(object command, params object[] parameters)
        {
            if (!m_Methods.ContainsKey(command))
            {
                return;
            }

            var method = m_Methods[command];
            method.Invoke(m_Target, parameters);
        }
    }
}
