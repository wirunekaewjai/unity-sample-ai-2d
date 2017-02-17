using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public class Fsm<T> : ICommander
    {
        private readonly List<ICommand> m_Observers = new List<ICommand>();

        private T m_Owner;
        private Dictionary<object, FsmState<T>> m_States;
        private object m_CurrentStateID;

        public T Owner
        {
            get { return m_Owner; }
        }

        public Fsm(T owner)
        {
            m_Owner = owner;
            m_States = new Dictionary<object, FsmState<T>>();
            m_CurrentStateID = null;
        }

        public void AddState(object stateID, FsmState<T> state)
        {
            state.Fsm = this;
            m_States.Add(stateID, state);
        }

        public void ChangeState(object nextStateID)
        {
            if (m_CurrentStateID != null)
            {
                m_States[m_CurrentStateID].OnExit();
            }

            m_CurrentStateID = nextStateID;

            if (m_CurrentStateID != null)
            {
                m_States[m_CurrentStateID].OnEnter();
            }
        }

        public void Execute(object command, params object[] parameters)
        {
            for(int i = 0; i < m_Observers.Count; i++)
            {
                m_Observers[i].OnExecute(command, parameters);
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