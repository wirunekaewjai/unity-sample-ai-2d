using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L09
{
    using IState = Wirune.L04.IState<Agent>;

    public class PatrolState : IState
    {
        #region IState implementation
        public void OnEnter(Agent agent)
        {

        }

        public void OnStay(Agent agent)
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

        public void OnExit(Agent agent)
        {

        }
        #endregion
    }
}

