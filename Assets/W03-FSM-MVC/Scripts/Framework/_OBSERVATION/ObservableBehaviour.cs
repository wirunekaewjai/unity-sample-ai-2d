using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

namespace Wirune.W03
{
    public class ObservableBehaviour : MonoBehaviour, IObservable, IObserver
    {
        private readonly List<IObserver> m_Observers = new List<IObserver>();

        protected virtual void Awake()
        {
            
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
            ObserveUtil.Invoke(this, eventID, parameters);
        }

        #endregion
    }
}

