using System;
using UnityEngine;

namespace Wirune.W03
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CallbackAttribute : Attribute
    {
        public readonly object tag;

        public CallbackAttribute()
        {
            this.tag = null;
        }

        public CallbackAttribute(object tag)
        {
            this.tag = tag;
        }
    }
}
