using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestView : View
    {
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Notify("RequestRandomPoint");
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                Notify("RequestSetPoint", 1000);
            }
        }

        public void OnPointChanged(int point)
        {
            Debug.Log("Point : " + point);
        }
    }
}
