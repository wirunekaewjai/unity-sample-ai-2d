using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
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
                if (remainingDistance >= enemy.radius + player.Radius)
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
                agent.moveSpeed = Mathf.Lerp(enemy.runSpeed, agent.moveSpeed, 0.2f);

                m_ElapsedTime += Time.deltaTime;

                if (m_ElapsedTime > 1f)
                {
                    enemy.Fsm.ChangeState(Enemy.PATROL_STATE);
                }
            }
        }

        public void OnExit(Enemy enemy)
        {
            
        }
        #endregion



//
//        private Queue<Vector2> m_SubPath;
//        private Node m_PlayerNode;
//
//        #region IState implementation
//        public void OnEnter(Enemy enemy)
//        {
//            m_ElapsedTime = 0;
//            m_MoveSpeed = enemy.MoveSpeed;
//
//            m_PlayerNode = enemy.Player.Node;
//            m_SubPath = enemy.GenerateSubPath(m_PlayerNode);
//        }
//
//        public void OnStay(Enemy enemy)
//        {
//            if (null != enemy.Player)
//            {
//                enemy.MoveSpeed = Mathf.Lerp(enemy.MoveSpeed, enemy.RunSpeed, 0.1f);
//
//                if (m_SubPath.Count > 0)
//                {
//                    PathFollow(enemy, enemy.Player.Radius);
//                }
//
//                if (m_PlayerNode != enemy.Player.Node)
//                {
//                    Queue<Vector2> nextPath = enemy.GenerateSubPath(m_PlayerNode, enemy.Player.Node);
//
//                    List<Vector2> paths = new List<Vector2>();
//                    paths.AddRange(m_SubPath);
//                    paths.AddRange(nextPath);
//
//                    PathSmoother.Smooth(paths, 0.5f);
//
//                    m_PlayerNode = enemy.Player.Node;
//                    m_SubPath = new Queue<Vector2>(paths);
//                }
//            }
//            else if (m_SubPath.Count > 0)
//            {
//                PathFollow(enemy, 0);
//            }
//            else
//            {
//                enemy.MoveSpeed = Mathf.Lerp(enemy.RunSpeed, enemy.MoveSpeed, 0.2f);
//                m_ElapsedTime += Time.deltaTime;
//
//                if (m_ElapsedTime > 1f)
//                {
//                    enemy.Fsm.ChangeState(Enemy.PATROL_STATE);
//                }
//            }
//        }
//
//        public void OnExit(Enemy enemy)
//        {
//            enemy.MoveSpeed = m_MoveSpeed;
//        }
//
//        public void OnDrawGizmos(Enemy enemy)
//        {
//            Gizmos.color = Color.red;
//            foreach (var subPath in m_SubPath)
//            {
//                Gizmos.DrawWireSphere(subPath, 0.2f);
//            }
//        }
//        #endregion
//
//        private void PathFollow(Enemy enemy, float radius)
//        {
//            Vector2 currentTarget = m_SubPath.Peek();
//            Vector2 currentPosition = enemy.Position;
//
//            Vector2 displacement = currentTarget - currentPosition;
//            float distance = displacement.magnitude - radius;
//
//            enemy.RotateTo(displacement);
//            enemy.MoveTo(displacement.normalized, distance);
//
//            if (distance <= enemy.StopDistance)
//            {
//                m_SubPath.Dequeue();
//            }
//        }
    }
}

