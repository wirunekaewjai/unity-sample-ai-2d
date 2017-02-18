using System.Collections.Generic;

namespace Wirune.W03.Test03
{
    public class AgentController : Controller<AgentModel, AgentView>
    {
        public Fsm<AgentController> Fsm { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Fsm = new Fsm<AgentController>(this);

            Fsm.AddState(1, new AgentControllerState1());
            Fsm.AddState(2, new AgentControllerState2());

            Fsm.ChangeState(1);
        }

        public override void OnNotify(object eventID, params object[] parameters)
        {
            base.OnNotify(eventID, parameters);
            Fsm.Notify(eventID, parameters);
        }
    }
}
