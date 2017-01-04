using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using IState = Wirune.L04.IState<Agent>;

    public class ChaseState : IState
    {
        private float m_ElapsedTime;

        #region IState implementation
        public void OnEnter(Agent agent)
        {
            m_ElapsedTime = 0;
        }

        public void OnStay(Agent agent)
        {
            if (null != agent.Player)
            {
                Vector2 currentTarget = agent.Player.transform.position;
                Vector2 currentPosition = agent.transform.position;

                Vector2 displacement = currentTarget - currentPosition;
                float distance = displacement.magnitude - agent.Player.radius;

                if (distance >= agent.StopDistance)
                {
                    agent.RotateTo(displacement);
                    agent.MoveForward(distance);
                }
                else
                {
                    agent.RotateTo(displacement);
                }
            }
            else
            {
                m_ElapsedTime += Time.deltaTime;

                if (m_ElapsedTime > 1f)
                {
                    agent.Fsm.ChangeState(Agent.PATROL_STATE);
                }
            }
        }

        public void OnExit(Agent agent)
        {

        }
        #endregion
    }
}

