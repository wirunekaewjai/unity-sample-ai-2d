using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentControllerState2 : FsmState<AgentController>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter : Controller State 2");
        }

        [Observe(AgentEvent.Resurrect)]
        void OnResurrect()
        {
            Owner.Model.Health += 1000;
            Fsm.ChangeState(1);
        }
    }
}

