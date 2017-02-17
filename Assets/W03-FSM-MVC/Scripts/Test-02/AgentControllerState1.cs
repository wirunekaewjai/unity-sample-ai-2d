using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentControllerState1 : FsmState<AgentController>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter : Controller State 1");
        }

        [Observe(AgentEvent.Collect)]
        void OnCollect(Collider c)
        {
            if (c.name == "Cube (1)")
            {
                HealthUp(5);
            }
            else if (c.name == "Cube (2)")
            {
                HealthDown(2);
            }
        }

        void HealthUp(int value)
        {
            Owner.Model.Health += value;
        }

        void HealthDown(int value)
        {
            Owner.Model.Health -= value;

            if (Owner.Model.Health == 0)
            {
                Owner.Notify(AgentEvent.Died);
                Fsm.ChangeState(2);
            }
        }
    }
}

