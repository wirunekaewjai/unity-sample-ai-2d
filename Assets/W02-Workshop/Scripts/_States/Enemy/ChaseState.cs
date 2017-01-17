using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class ChaseState : IState<Enemy>
    {
        private float m_ElapsedTime;
        private float m_MoveSpeed;

        #region IState implementation
        public void OnEnter(Enemy enemy)
        {
            m_ElapsedTime = 0;
            m_MoveSpeed = enemy.agent.moveSpeed;
        }

        public void OnStay(Enemy enemy)
        {
            GraphAgent agent = enemy.agent;

            if (null != enemy.Player)
            {
                agent.moveSpeed = Mathf.Lerp(agent.moveSpeed, enemy.runSpeed, 0.1f);

                Player player = enemy.Player;

                Vector2 position = agent.Position;
                Vector2 target = player.agent.Position;

                float remainingDistance = Vector2.Distance(position, target);
                if (remainingDistance >= enemy.agent.radius + player.agent.radius)
                {
                    agent.Destination = target;
                }
                else
                {
                    agent.Destination = position;
                }
            }
            else
            {
                agent.moveSpeed = Mathf.Lerp(agent.moveSpeed, m_MoveSpeed, 0.2f);

                if (agent.IsReached)
                {
                    m_ElapsedTime += Time.deltaTime;

                    if (m_ElapsedTime > 2f)
                    {
                        enemy.Fsm.ChangeState(Enemy.PATROL_STATE);
                    }
                }
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
        #endregion
    }
}

