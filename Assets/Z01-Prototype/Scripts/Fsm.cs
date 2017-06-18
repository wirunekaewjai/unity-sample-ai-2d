using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.Z01
{
    public class Fsm : MonoBehaviour 
    {
        [SerializeField]
        private List<string> m_EventNames = new List<string>();

        [SerializeField]
        private List<FsmState> m_States = new List<FsmState>();

        private void Reset()
        {
            m_EventNames.Clear();
            m_States.Clear();

            AddEventName("Start");
        }

        public void AddEventName(string eventName)
        {
            m_EventNames.Add(eventName);
        }

        public void RemoveEventName(string eventName)
        {
            m_EventNames.Remove(eventName);
        }

        public void RemoveEventNameAt(int index)
        {
            m_EventNames.RemoveAt(index);
        }

        public string GetEventName(int index)
        {
            return m_EventNames[index];
        }

        public void SetEventName(int index, string eventName)
        {
            m_EventNames[index] = eventName;
        }

        public bool ContainsEventName(string eventName)
        {
            return m_EventNames.Contains(eventName);
        }

        public int EventNameCount
        {
            get { return m_EventNames.Count; }
        }

        public void AddState(FsmState state)
        {
            m_States.Add(state);
        }

        public void RemoveState(FsmState state)
        {
            m_States.Remove(state);
        }

        public void RemoveStateAt(int index)
        {
            m_States.RemoveAt(index);
        }

        public FsmState GetState(int index)
        {
            return m_States[index];
        }

        public int StateCount
        {
            get { return m_States.Count; }
        }
    }
}