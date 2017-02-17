using System;
using UnityEngine;

namespace Wirune.W03
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandCallback : Attribute
    {
        public readonly object id;

        public CommandCallback()
        {
            this.id = null;
        }

        public CommandCallback(object id)
        {
            this.id = id;
        }
    }
}
