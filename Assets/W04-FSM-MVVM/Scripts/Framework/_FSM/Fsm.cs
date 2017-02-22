using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04
{
    public sealed class Fsm<TOwner>
    {
        private TOwner m_Owner;
        public TOwner Owner { get { return m_Owner; } }

        private Dictionary<object, FsmState<TOwner>> m_States;
        private object m_CurrentStateID;

        private Stack<object> m_PreviousStatesIDs;

        public Fsm(TOwner owner)
        {
            m_Owner = owner;

            m_States = new Dictionary<object, FsmState<TOwner>>();
            m_CurrentStateID = null;

            m_PreviousStatesIDs = new Stack<object>();
        }

        public void CreateState<TState>(object stateID)
            where TState : FsmState<TOwner>
        {
            var state = System.Activator.CreateInstance<TState>();

            state.Fsm = this;
            state.OnCreate();

            m_States.Add(stateID, state);
        }

        public void ChangeState(object nextStateID)
        {
            if (m_CurrentStateID != null)
            {
                m_States[m_CurrentStateID].OnExit();
                m_PreviousStatesIDs.Push(m_CurrentStateID);
            }

            m_CurrentStateID = nextStateID;

            if (m_CurrentStateID != null)
            {
                m_States[m_CurrentStateID].OnEnter();
            }
        }

        public bool GoToPreviousState()
        {
            if (m_PreviousStatesIDs.Count > 0)
            {
                object previousStateID = m_PreviousStatesIDs.Pop();

                if (m_CurrentStateID != null)
                {
                    m_States[m_CurrentStateID].OnExit();
                }

                m_CurrentStateID = previousStateID;

                if (m_CurrentStateID != null)
                {
                    m_States[m_CurrentStateID].OnEnter();
                }

                return true;
            }

            return false;
        }


        public void ClearPreviousStates()
        {
            m_PreviousStatesIDs.Clear();
        }
    }
}

