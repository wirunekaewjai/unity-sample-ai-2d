using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public class Fsm<T> : IObservable
    {
        private List<IObserver> m_Observers = new List<IObserver>();

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
    }
}