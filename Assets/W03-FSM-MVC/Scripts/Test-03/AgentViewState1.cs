using UnityEngine;

namespace Wirune.W03.Test03
{
    public class AgentViewState1 : FsmState<AgentView>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Enter : View State 1");
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

            Vector3 velocity = new Vector3(h, v, 0) * Owner.Speed * Time.deltaTime;
            Owner.transform.Translate(velocity, Space.World);
        }


    }
}

