using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public class Controller<TModel, TView> : ObservableBehaviour
        where TModel : Model
        where TView : View
    {
        [SerializeField]
        private TModel m_Model;
        public TModel Model { get { return m_Model; } }

        [SerializeField]
        private TView m_View;
        public TView View { get { return m_View; } }

        protected virtual void OnEnable()
        {
            // Controller <-> View
            this.Register(m_View);
            m_View.Register(this);

            // Model -> View
            m_Model.Register(m_View);
        }

        protected virtual void OnDisable()
        {
            this.Unregister(m_View);
            m_View.Unregister(this);

            m_Model.Unregister(m_View);
        }
    }
}
