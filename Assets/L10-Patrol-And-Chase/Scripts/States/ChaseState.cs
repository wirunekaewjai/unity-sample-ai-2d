using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using IState = Wirune.L04.IState<Agent>;
    using Player = Wirune.L06.Player;

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
                Vector2 position = agent.Position;
                Vector2 target = agent.Player.Position;
                Vector2 velocity = agent.Seek(target);

                float remainingDistance = Vector2.Distance(target, position);
                if (remainingDistance >= agent.Player.Radius + agent.radius)
                {
                    agent.Position = position + velocity;
                }

                agent.Rotate(velocity);
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

