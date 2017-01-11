using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L09
{
    using IState = Wirune.L04.IState<Agent>;
    using Point = Wirune.L07.Point;

    public class PatrolState : IState
    {
        #region IState implementation
        public void OnEnter(Agent agent)
        {

        }

        public void OnStay(Agent agent)
        {
            Point point = agent.GetCurrentPoint();

            Vector2 position = agent.Position;
            Vector2 target = point.Position;
            Vector2 velocity = agent.Seek(target);

            float remainingDistance = Vector2.Distance(target, position);
            if (remainingDistance >= agent.stoppingDistance)
            {
                agent.Position = position + velocity;
                agent.Rotate(velocity);
            }
            else if (point is ObservablePoint)
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

