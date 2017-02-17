using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public class View : Commander
    {
        public readonly Command observer = new Command();

        protected virtual void Awake()
        {
            observer.BindCallbackAttribute(this);
        }
    }
}

