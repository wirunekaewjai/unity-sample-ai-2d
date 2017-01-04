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
            if (agent.IsTargetInRange())
            {
                Vector2 displacement = agent.target.position - agent.transform.position;
                Vector2 direction = displacement.normalized;

                agent.RotateTo(direction);
                agent.MoveForward();
            }
            else
            {
                agent.fsm.ChangeState(Agent.OBSERVE_STATE);
            }
        }

        public void OnExit(Agent agent)
        {

        }
        #endregion

    }
}

