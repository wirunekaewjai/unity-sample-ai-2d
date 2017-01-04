using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L08
{
    public class SeekState : Wirune.L04.IState<Agent>
    {
        #region IState implementation
        public void OnEnter(Agent agent)
        {

        }

        public void OnStay(Agent agent)
        {
            if (agent.HasPlayer())
            {
                Vector2 currentTarget = agent.GetPlayer().transform.position;
                Vector2 currentPosition = agent.transform.position;

                Vector2 displacement = currentTarget - currentPosition;
                Vector2 direction = displacement.normalized;

                float distance = displacement.magnitude - agent.GetPlayer().radius;

                agent.RotateTo(direction);
                agent.MoveForward(distance);
            }
            else
            {
                agent.GetFsm().ChangeState(Agent.OBSERVE_STATE);
            }

//            if (agent.IsTargetInRange())
//            {
//                Vector2 displacement = agent.target.position - agent.transform.position;
//                Vector2 direction = displacement.normalized;
//
//                agent.RotateTo(direction);
//                agent.MoveForward();
//            }
//            else
//            {
//                agent.fsm.ChangeState(Agent.OBSERVE_STATE);
//            }
        }

        public void OnExit(Agent agent)
        {

        }
        #endregion

    }
}

