using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W04.Test01
{
    public class AgentControllerState1 : UpdatableControllerState<AgentModel, AgentView>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 1");

            Model.DiedEvent += OnDied;
            View.TriggerEnterEvent += OnTriggerEnter;
        }

        public override void OnExit()
        {
            base.OnExit();

            Model.DiedEvent -= OnDied;
            View.TriggerEnterEvent -= OnTriggerEnter;
        }

        private void OnUpdate()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var direction = new Vector3(h, v, 0);
            var velocity = direction * Model.Speed;

            View.Move(velocity);
        }

        private void OnTriggerEnter(Collider c)
        {
            if (c.name == "Cube (1)")
            {
                Model.Health += Random.Range(2, 5);
            }
            else if (c.name == "Cube (2)")
            {
                Model.Health -= Random.Range(2, 5);
            }
        }

        private void OnDied()
        {
            Debug.Log("Died");
            Controller.ChangeState(2);
        }
    }
}

