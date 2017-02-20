using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    public class AgentControllerState2 : FsmUpdatableState<AgentController>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 2");
            Owner.Model.ResurrectEvent += OnResurrect;
        }

        public override void OnExit()
        {
            base.OnExit();
            Owner.Model.ResurrectEvent -= OnResurrect;
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Owner.Model.Resurrect();
            }
        }

        private void OnResurrect()
        {
            Debug.Log("Resurrect");
            Fsm.ChangeState(1);
        }
    }
}

