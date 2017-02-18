using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test01
{
    public class AgentController : Controller<AgentModel, AgentView>
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            CreateState<AgentControllerState1>(1);
            CreateState<AgentControllerState2>(2);

            ChangeState(1);
        }

        protected override void OnStart()
        {
            base.OnStart();

            Model.MaxHealthChangedEvent += View.SetMaxHealth;
            Model.HealthChangedEvent += View.SetHealth;
        }
    }
}
