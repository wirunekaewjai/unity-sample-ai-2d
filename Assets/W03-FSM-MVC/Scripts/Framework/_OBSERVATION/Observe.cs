using System;
using System.Reflection;
using System.Collections.Generic;

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
    }
}
