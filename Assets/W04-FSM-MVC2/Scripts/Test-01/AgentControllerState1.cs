using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    public class AgentControllerState1 : FsmUpdatableState<AgentController>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 1");

            Owner.Model.DiedEvent += OnDied;
            Owner.View.TriggerEnterEvent += OnTriggerEnter;
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.Model.DiedEvent -= OnDied;
            Owner.View.TriggerEnterEvent -= OnTriggerEnter;
        }

        private void OnUpdate()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var direction = new Vector3(h, v, 0);
            var velocity = direction * Owner.Model.Speed;

            Owner.View.Move(velocity);
        }

        private void OnTriggerEnter(Collider c)
        {
            if (c.name == "Cube (1)")
            {
                Owner.Model.Health += Random.Range(2, 5);
            }
            else if (c.name == "Cube (2)")
            {
                Owner.Model.Health -= Random.Range(2, 5);
            }
        }

        private void OnDied()
        {
            Debug.Log("Died");
            Fsm.ChangeState(2);
        }
    }
}

