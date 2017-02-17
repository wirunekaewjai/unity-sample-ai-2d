using System;
using UnityEngine;

namespace Wirune.W03
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CommandCallback : Attribute
    {
        public readonly object command;

        public CommandCallback()
        {
            this.command = null;
        }

        public CommandCallback(object command)
        {
            this.command = command;
        }
    }
}
