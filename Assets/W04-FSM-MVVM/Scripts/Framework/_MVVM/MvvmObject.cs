using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04
{
    [System.Serializable]
    public class MvvmObject : IExecutable
    {
        private readonly List<IExecutable> m_Executables = new List<IExecutable>();

        #region IExecutable implementation
        public void Register(IExecutable executable)
        {
            if (!m_Executables.Contains(executable))
            {
                m_Executables.Add(executable);
            }
        }

        public void Unregister(IExecutable executable)
        {
            m_Executables.Remove(executable);
        }

        public void Link(IExecutable executable)
        {
            this.Register(executable);
            executable.Register(this);
        }

        public void Unlink(IExecutable executable)
        {
            this.Unregister(executable);
            executable.Unregister(this);
        }

        public void Execute(object command, params object[] parameters)
        {
            for(int i = 0; i < m_Executables.Count; i++)
            {
                Binder.Invoke(m_Executables[i], command, parameters);
            }
        }
        #endregion
    }
}