using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class EatState : IState<Worker>
    {
        #region IState implementation
        public void OnEnter(Worker owner)
        {
            Debug.Log("Enter Eat State");
        }

        public void OnStay(Worker owner)
        {
            Debug.Log("Eating...");

            owner.fullness += 1;

            if (owner.fullness >= 10)
            {
                owner.Fsm.ChangeState(Worker.WORK_STATE);
            }
        }

        public void OnExit(Worker owner)
        {
            Debug.Log("Exit Eat State");
        }
        #endregion
    }
}