using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    public class AgentControllerState2 : UpdatableControllerState<AgentModel, AgentView>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 2");
            Model.ResurrectEvent += OnResurrect;
        }

        public override void OnExit()
        {
            base.OnExit();
            Model.ResurrectEvent -= OnResurrect;
        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Model.Resurrect();
            }
        }

        private void OnResurrect()
        {
            Debug.Log("Resurrect");
            Controller.ChangeState(1);
        }
    }
}

