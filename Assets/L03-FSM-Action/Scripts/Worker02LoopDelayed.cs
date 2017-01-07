using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L03
{
    public class Worker02LoopDelayed : MonoBehaviour 
    {
        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        [Space]
        public float delay = 1f;

        private readonly Stack<Action> m_States = new Stack<Action>();

        void Awake ()
        {
            m_States.Push(DoWorkState);
        }

        IEnumerator Start () 
        {
            while (true)
            {
                Action state = m_States.Peek();
                state.Invoke();

                yield return new WaitForSeconds(delay);
            }
        }

        void DoEatState()
        {
            Debug.Log("Eating..."); 

            fullness += 1;

            if (fullness >= 10)
            {
                m_States.Pop();
            }
        }

        void DoSleepState()
        {
            Debug.Log("Sleeping...");

            stamina += 1;

            if (stamina >= 10)
            {
                m_States.Pop();
            }
        }

        void DoRelaxState()
        {
            Debug.Log("Relaxing...");

            happiness += 1;

            if (happiness >= 10)
            {
                m_States.Pop();
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
                m_States.Push(DoEatState);
            }

            if (stamina <= 0)
            {
                m_States.Push(DoSleepState);
            }

            if (happiness <= 0)
            {
                m_States.Push(DoRelaxState);
            }
        }
    }
}
