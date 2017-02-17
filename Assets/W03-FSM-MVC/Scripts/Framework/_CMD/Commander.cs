using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public class Commander : MonoBehaviour, ICommander
    {
        private readonly List<ICommand> m_Observers = new List<ICommand>();

        public void Execute(object command, params object[] parameters)
        {
            foreach (var observer in m_Observers)
            {
                observer.OnExecute(command, parameters);
            }
        }

        public void Register(ICommand observer)
        {
            if (!m_Observers.Contains(observer))
            {
                m_Observers.Add(observer);
            }
        }

        public void Unregister(ICommand observer)
        {
            m_Observers.Remove(observer);
        }
    }
}

