using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    public class AgentControllerState2 : ControllerState<AgentModel, AgentView>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 2");
            Looper.RegisterUpdate(OnUpdate);

            Model.ResurrectEvent += OnResurrect;
        }

        public override void OnExit()
        {
            base.OnExit();
            Looper.UnregisterUpdate(OnUpdate);

            Model.ResurrectEvent -= OnResurrect;
        }

        void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Model.Resurrect();
            }
        }

        void OnResurrect()
        {
            Debug.Log("Resurrect");
            Controller.ChangeState(1);
        }
    }
}

