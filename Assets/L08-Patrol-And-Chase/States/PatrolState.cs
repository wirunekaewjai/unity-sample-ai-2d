using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L08
{
    public class PatrolState : Wirune.L04.IState<Agent>
    {
        #region IState implementation
        public void OnEnter(Agent agent)
        {
            
        }

        public void OnStay(Agent agent)
        {
            if (agent.HasPlayer())
            {
                agent.GetFsm().ChangeState(Agent.SEEK_STATE);
            }
            else
            {
                Vector2 currentTarget = agent.GetCurrentPoint();
                Vector2 currentPosition = agent.transform.position;

                Vector2 displacement = currentTarget - currentPosition;
                float distance = displacement.magnitude;

                if (distance > 0.05f)
                {
                    agent.RotateTo(displacement);
                    agent.MoveForward(distance);
                }
                else
                {
                    agent.NextPoint();
                    agent.GetFsm().ChangeState(Agent.OBSERVE_STATE);
                }
            }

//            if (agent.IsTargetInRange())
//            {
//                agent.fsm.ChangeState(Agent.SEEK_STATE);
//            }
//            else
//            {
//                Vector2 currentPoint = agent.GetCurrentPoint();
//                Vector2 currentPosition = agent.transform.position;
//
//                Vector2 displacement = currentPoint - currentPosition;
//                Vector2 direction = displacement.normalized;
//
//                agent.RotateTo(direction);
//                agent.MoveForward();
//
//                float distance = displacement.magnitude;
//                if (distance < agent.stopDistance)
//                {
//                    agent.GoToNextPoint();
//                    agent.fsm.ChangeState(Agent.OBSERVE_STATE);
//                }
//            }
        }

        public void OnExit(Agent agent)
        {
            
        }
        #endregion
    }
}

