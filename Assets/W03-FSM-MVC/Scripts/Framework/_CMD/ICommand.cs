using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public interface ICommand
    {
        void BindCallbackAttribute(object target);
        void OnExecute(object id, params object[] parameters);
    }
}
