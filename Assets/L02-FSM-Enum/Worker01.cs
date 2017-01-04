using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L02
{
    public class Worker01 : MonoBehaviour 
    {
        public enum State
        {
            Work, Eat, Sleep, Relax
        }

        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        public State state = State.Work;

        void Update () 
        {
            if (state == State.Eat)
            {
                Debug.Log("Eating..."); 

                fullness += 1;

                if (fullness >= 10)
                {
                    state = State.Work;
                }
            }
            else if (state == State.Sleep)
            {
                Debug.Log("Sleeping...");

                stamina += 1;

                if (stamina >= 10)
                {
                    state = State.Work;
                }
            }
            else if (state == State.Relax)
            {
                Debug.Log("Relaxing...");

                happiness += 1;

                if (happiness >= 10)
                {
                    state = State.Work;
                }
            }
            else
            {
                Debug.Log("Working...");

                fullness -= 3;
                stamina -= 2;
                happiness -= 1;

                if (fullness <= 0)
                {
                    state = State.Eat;
                }

                if (stamina <= 0)
                {
                    state = State.Sleep;
                }

                if (happiness <= 0)
                {
                    state = State.Relax;
                }
            }
        }
    }
}
