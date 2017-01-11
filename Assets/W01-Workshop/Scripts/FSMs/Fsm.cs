using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class Fsm<T> where T : MonoBehaviour
    {
        private T m_Owner;
        private Dictionary<byte, IState<T>> m_States;
        private byte m_CurrentStateID;

        public Fsm(T owner)
        {
            m_Owner = owner;
            m_States = new Dictionary<byte, IState<T>>();
            m_CurrentStateID = 0;
        }

        public void AddState(byte stateID, IState<T> state)
        {
            m_States.Add(stateID, state);
        }

        public void Update()
        {
            if (m_CurrentStateID > 0)
            {
                m_States[m_CurrentStateID].OnStay(m_Owner);
            }
        }

        public void ChangeState(byte nextStateID)
        {
            if (m_CurrentStateID > 0)
            {
                m_States[m_CurrentStateID].OnExit(m_Owner);
            }

            m_CurrentStateID = nextStateID;

            if (m_CurrentStateID > 0)
            {
                m_States[m_CurrentStateID].OnEnter(m_Owner);
            }
        }
    }
}