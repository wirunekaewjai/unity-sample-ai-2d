using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L04
{
    public class WorkState : IState<Worker>
    {
        #region IState implementation
        public void OnEnter(Worker owner)
        {
            Debug.Log("Enter Work State");
        }

        public void OnStay(Worker owner)
        {
            Debug.Log("Working...");

            owner.fullness -= 3;
            owner.stamina -= 2;
            owner.happiness -= 1;

            if (owner.fullness <= 0)
            {
                owner.fsm.ChangeState(Worker.EAT_STATE);
            }

            if (owner.stamina <= 0)
            {
                owner.fsm.ChangeState(Worker.SLEEP_STATE);
            }

            if (owner.happiness <= 0)
            {
                owner.fsm.ChangeState(Worker.RELAX_STATE);
            }
        }

        public void OnExit(Worker owner)
        {
            Debug.Log("Exit Work State");
        }
        #endregion
    }
}