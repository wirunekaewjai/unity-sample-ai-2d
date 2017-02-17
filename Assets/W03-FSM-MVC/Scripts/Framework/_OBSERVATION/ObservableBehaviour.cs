using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

using UnityEngine;

namespace Wirune.W03
{
    public class ObservableBehaviour : MonoBehaviour, IObservable, IObserver
    {
        private readonly List<IObserver> m_Observers = new List<IObserver>();
        private Dictionary<object, MethodInfo> m_Observes = new Dictionary<object, MethodInfo>();

        protected virtual void Awake()
        {
            m_Observes = Observe.FindObserveMethods(this);
        }
        #region IObservable implementation

        public void Register(IObserver observer)
        {
            if (!m_Observers.Contains(observer))
            {
                m_Observers.Add(observer);
            }
        }

        public void Unregister(IObserver observer)
        {
            m_Observers.Remove(observer);
        }

        public void Notify(object eventID, params object[] parameters)
        {
            for (int i = 0; i < m_Observers.Count; i++)
            {
                m_Observers[i].OnNotify(eventID, parameters);
            }
        }

        #endregion

        #region IObserver implementation

        public virtual void OnNotify(object eventID, params object[] parameters)
        {
            if (m_Observes.ContainsKey(eventID))
            {
                var method = m_Observes[eventID];
                method.Invoke(this, parameters);
            }
        }

        #endregion
    }
}

