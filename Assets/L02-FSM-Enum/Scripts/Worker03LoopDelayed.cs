using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L02
{
    public class Worker03LoopDelayed : MonoBehaviour 
    {
        public enum State
        {
            Work, Eat, Sleep, Relax
        }

        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        [Space]
        public float delay = 1f;

        private readonly Stack<State> m_States = new Stack<State>();

        void Awake()
        {
            m_States.Push(State.Work);
        }

        IEnumerator Start ()
        {
            while (true)
            {
                State state = m_States.Peek();

                if (state == State.Eat)
                    DoEatState();
                else if (state == State.Sleep)
                    DoSleepState();
                else if (state == State.Relax)
                    DoRelaxState();
                else
                    DoWorkState();

                yield return new WaitForSeconds(1f);
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
                m_States.Push(State.Eat);
            }

            if (stamina <= 0)
            {
                m_States.Push(State.Sleep);
            }

            if (happiness <= 0)
            {
                m_States.Push(State.Relax);
            }
        }
    }
}
