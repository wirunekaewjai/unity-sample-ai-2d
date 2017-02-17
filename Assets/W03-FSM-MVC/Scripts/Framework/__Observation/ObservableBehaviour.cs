using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public class ObservableBehaviour : MonoBehaviour, IObservable
    {
        private readonly List<IObserver> m_Observers = new List<IObserver>();

        public void Execute(object command, params object[] parameters)
        {
            foreach (var observer in m_Observers)
            {
                observer.OnExecute(command, parameters);
            }
        }

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
    }
}

