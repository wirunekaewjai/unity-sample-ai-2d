using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L01
{
    public class Worker02LoopDelayed : MonoBehaviour 
    {
        public int fullness = 10;
        public int stamina = 10;
        public int happiness = 10;

        public bool isHungry;
        public bool isTired;
        public bool isUnhappy;

        [Space]
        public float delay = 1f;

        IEnumerator Start()
        {
            while (true)
            {
                if (isHungry)
                {
                    Debug.Log("Eating..."); 

                    fullness += 1;

                    if (fullness >= 10)
                    {
                        isHungry = false;
                    }
                }
                else if (isTired)
                {
                    Debug.Log("Sleeping...");

                    stamina += 1;

                    if (stamina >= 10)
                    {
                        isTired = false;
                    }
                }
                else if (isUnhappy)
                {
                    Debug.Log("Relaxing...");

                    happiness += 1;

                    if (happiness >= 10)
                    {
                        isUnhappy = false;
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
                        isHungry = true;
                    }

                    if (stamina <= 0)
                    {
                        isTired = true;
                    }

                    if (happiness <= 0)
                    {
                        isUnhappy = true;
                    }
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }
}
