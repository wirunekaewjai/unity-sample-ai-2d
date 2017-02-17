using UnityEngine;

namespace Wirune.W03.Test02
{
    public class TestState2 : FsmState<TestFSM>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 2");
            Looper.RegisterUpdate(OnUpdate);
        }

        public override void OnExit()
        {
            base.OnExit();

            Looper.UnregisterUpdate(OnUpdate);
        }

        void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Resurrect();
            }
        }

        void Resurrect()
        {
            Owner.Health += 1000;
            Fsm.ChangeState(1);
        }
    }
}

