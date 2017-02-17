using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;

namespace Wirune.W03
{
    public class Controller<TModel> : MonoBehaviour 
        where TModel : Model
    {
        private readonly Command m_Observer = new Command();

        [SerializeField]
        private TModel m_Model;
        public TModel Model { get { return m_Model; } }

        [SerializeField]
        private List<View> m_Views = new List<View>();
        public List<View> Views { get { return m_Views; } }

        protected virtual void Awake()
        {
            m_Observer.BindCallbackAttribute(this);
        }

        protected virtual void OnEnable()
        {
            foreach (var view in m_Views)
            {
                view.Register(m_Observer);
                m_Model.Register(view.observer);
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var view in m_Views)
            {
                view.Unregister(m_Observer);
                m_Model.Unregister(view.observer);
            }
        }

    }
}
