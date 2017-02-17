using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public interface IObserver
    {
        void BindCallbackAttribute(object target);
        void OnExecute(object command, params object[] parameters);
    }
}
