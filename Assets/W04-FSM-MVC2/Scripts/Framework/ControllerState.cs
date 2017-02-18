using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Wirune.W04
{
    public class ControllerState<TModel, TView>
        where TModel : IModel
        where TView : IView
    {
        public Controller<TModel, TView> Controller { get; internal set; }
        public TModel Model { get { return Controller.Model; } }
        public TView View { get { return Controller.View; } }

        public virtual void OnCreate()
        {
            
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}

