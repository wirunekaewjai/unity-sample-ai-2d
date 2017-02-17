using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentView : View
    {
        public float speed = 2f;
        public Fsm<AgentView> Fsm { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Fsm = new Fsm<AgentView>(this);
            Fsm.AddState(1, new AgentViewState1());
            Fsm.AddState(2, new AgentViewState2());

            Fsm.ChangeState(1);
        }

        void OnTriggerEnter(Collider c)
        {
            if (c.name == "Cube (1)")
            {
                Fsm.Execute("Recover");
            }
            else if (c.name == "Cube (2)")
            {
                Fsm.Execute("TakeDamage", 2);
            }
        }

        [CallbackAttribute("OnDied")]
        void OnDied()
        {
            Fsm.ChangeState(2);
        }
    }
}
