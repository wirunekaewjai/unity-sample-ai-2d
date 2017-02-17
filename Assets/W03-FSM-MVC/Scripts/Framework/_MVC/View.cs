using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public class View : ObservableBehaviour
    {
        public readonly Observer observer = new Observer();

        protected virtual void Awake()
        {
            observer.BindCallbackAttribute(this);
        }
    }
}

