using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class RelaxState : IState<Worker>
    {
        #region IState implementation
        public void OnEnter(Worker owner)
        {
            Debug.Log("Enter Relax State");
        }

        public void OnStay(Worker owner)
        {
            Debug.Log("Relaxing...");

            owner.happiness += 1;

            if (owner.happiness >= 10)
            {
                owner.fsm.ChangeState(Worker.WORK_STATE);
            }
        }

        public void OnExit(Worker owner)
        {
            Debug.Log("Exit Relax State");
        }
        #endregion
    }
}