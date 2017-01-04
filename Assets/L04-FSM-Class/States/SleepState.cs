using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class SleepState : IState<Worker>
    {
        #region IState implementation
        public void OnEnter(Worker owner)
        {
            Debug.Log("Enter Sleep State");
        }

        public void OnStay(Worker owner)
        {
            Debug.Log("Sleeping...");

            owner.stamina += 1;

            if (owner.stamina >= 10)
            {
                owner.fsm.ChangeState(Worker.WORK_STATE);
            }
        }

        public void OnExit(Worker owner)
        {
            Debug.Log("Exit Sleep State");
        }
        #endregion
    }
}