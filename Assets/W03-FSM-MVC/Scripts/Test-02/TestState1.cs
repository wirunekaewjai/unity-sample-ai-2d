using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test02
{
    public class TestState1 : FsmState<TestFSM>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : State 1");
            Looper.RegisterUpdate(OnUpdate);
        }

        public override void OnExit()
        {
            base.OnExit();
            Looper.UnregisterUpdate(OnUpdate);
        }

        void OnUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 velocity = new Vector3(h, v, 0) * Owner.speed * Time.deltaTime;
            Owner.transform.Translate(velocity, Space.World);
        }

        [Observe("Collect")]
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
            Owner.Health += value;
        }

        void HealthDown(int value)
        {
            Owner.Health -= value;

            if (Owner.Health == 0)
            {
                Died();
            }
        }

        void Died()
        {
            Debug.Log("Died");
            Fsm.ChangeState(2);
        }
    }
}

