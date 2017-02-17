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
        private readonly Command m_Command = new Command();

        [SerializeField]
        private TModel m_Model;
        public TModel Model { get { return m_Model; } }

        [SerializeField]
        private List<View> m_Views = new List<View>();
        public List<View> Views { get { return m_Views; } }

        protected virtual void Awake()
        {
            m_Command.BindCallbackAttribute(this);
        }

        protected virtual void OnEnable()
        {
            foreach (var view in m_Views)
            {
                view.Register(m_Command);
                m_Model.Register(view.command);
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var view in m_Views)
            {
                view.Unregister(m_Command);
                m_Model.Unregister(view.command);
            }
        }

    }
}
