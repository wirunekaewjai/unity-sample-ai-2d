using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test01
{
    public class AgentController : Controller<AgentModel, AgentView>
    {
        protected override void Awake()
        {
            base.Awake();

            CreateState<AgentControllerState1>(1);
            CreateState<AgentControllerState2>(2);

            ChangeState(1);
        }

        private void Start()
        {
            Model.MaxHealthChangedEvent += View.SetMaxHealth;
            Model.HealthChangedEvent += View.SetHealth;
            Model.Start();
        }
    }
}
