using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public class Commander : MonoBehaviour, ICommander
    {
        private readonly List<ICommand> m_Commands = new List<ICommand>();

        public void Execute(object id, params object[] parameters)
        {
            for (int i = 0; i < m_Commands.Count; i++)
            {
                m_Commands[i].OnExecute(id, parameters);
            }
        }

        public void Register(ICommand command)
        {
            if (!m_Commands.Contains(command))
            {
                m_Commands.Add(command);
            }
        }

        public void Unregister(ICommand command)
        {
            m_Commands.Remove(command);
        }
    }
}

