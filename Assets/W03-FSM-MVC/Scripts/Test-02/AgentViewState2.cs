using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentViewState2 : FsmState<AgentView>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : View State 2");
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
                Owner.Notify(AgentEvent.Resurrect);
                Fsm.ChangeState(1);
            }
        }
    }
}

