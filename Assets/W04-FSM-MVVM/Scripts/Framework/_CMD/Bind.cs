using System;
using UnityEngine;

namespace Wirune.W04
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class Bind : Attribute
    {
        public readonly object command;

        public Bind()
        {
            this.command = null;
        }

        public Bind(object command)
        {
            this.command = command;
        }
    }
}
