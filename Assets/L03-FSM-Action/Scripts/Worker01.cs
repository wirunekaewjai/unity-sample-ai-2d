using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L03
{
    public class Worker01 : MonoBehaviour 
    {
        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        private Action m_CurrentState;

        void Awake ()
        {
            m_CurrentState = DoWorkState;
        }

        void Update () 
        {
            m_CurrentState.Invoke();
        }

        void DoEatState()
        {
            Debug.Log("Eating..."); 

            fullness += 1;

            if (fullness >= 10)
            {
                m_CurrentState = DoWorkState;
            }
        }

        void DoSleepState()
        {
            Debug.Log("Sleeping...");

            stamina += 1;

            if (stamina >= 10)
            {
                m_CurrentState = DoWorkState;
            }
        }

        void DoRelaxState()
        {
            Debug.Log("Relaxing...");

            happiness += 1;

            if (happiness >= 10)
            {
                m_CurrentState = DoWorkState;
            }
        }

        void DoWorkState()
        {
            Debug.Log("Working...");

            fullness -= 3;
            stamina -= 2;
            happiness -= 1;

            if (fullness <= 0)
            {
                m_CurrentState = DoEatState;
            }

            if (stamina <= 0)
            {
                m_CurrentState = DoSleepState;
            }

            if (happiness <= 0)
            {
                m_CurrentState = DoRelaxState;
            }
        }
    }
}
