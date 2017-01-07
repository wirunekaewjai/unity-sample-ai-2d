using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L01
{
    public class Worker01 : MonoBehaviour 
    {
        public bool isHungry;
        public bool isTired;
        public bool isUnhappy;

        void Update () 
        {
            if (isHungry)
            {
                Debug.Log("Eating..."); 
            }
            else if (isTired)
            {
                Debug.Log("Sleeping...");
            }
            else if (isUnhappy)
            {
                Debug.Log("Relaxing...");
            }
            else
            {
                Debug.Log("Working...");
            }
        }
    }
}
