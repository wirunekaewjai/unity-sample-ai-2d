using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using IState = Wirune.L04.IState<Agent>;
    using ObservablePoint = Wirune.L09.ObservablePoint;

    public class PatrolState : IState
    {
        #region IState implementation
        public void OnEnter(Agent agent)
        {

        }

        public void OnStay(Agent agent)
        {
            if (null != agent.Player)
            {
                agent.Fsm.ChangeState(Agent.CHASE_STATE);
            }
            else
            {
                Vector2 currentTarget = agent.GetCurrentPoint().Position;
                Vector2 currentPosition = agent.transform.position;

                Vector2 displacement = currentTarget - currentPosition;
                float distance = displacement.magnitude;

                if (distance > 0.05f)
                {
                    agent.RotateTo(displacement);
                    agent.MoveForward(distance);
                }
                else if (agent.GetCurrentPoint() is ObservablePoint)
                {
                    agent.Fsm.ChangeState(Agent.OBSERVE_STATE);
                }
                else
                {
                    agent.NextPoint();
                }
            }
        }

        public void OnExit(Agent agent)
        {

        }
        #endregion
    }
}

