using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class PatrolState : IState<Enemy>
    {
        private Queue<Vector2> m_SubPath = new Queue<Vector2>();
        
        #region IState implementation
        public void OnEnter(Enemy enemy)
        {
            m_SubPath = enemy.GenerateSubPath(enemy.GetCurrentPoint().Node);
        }

        public void OnStay(Enemy enemy)
        {
            if (null != enemy.Player)
            {
                enemy.Fsm.ChangeState(Enemy.CHASE_STATE);
            }
            else if (m_SubPath.Count > 0)
            {
                Vector2 currentTarget = m_SubPath.Peek();
                Vector2 currentPosition = enemy.Position;

                Vector2 displacement = currentTarget - currentPosition;
                float distance = displacement.magnitude;

                enemy.RotateTo(displacement);
                enemy.MoveTo(displacement.normalized, distance);

                if (distance < 0.05f)
                {
                    m_SubPath.Dequeue();
                }
            }
            else if (enemy.GetCurrentPoint() is ObservablePoint)
            {
                enemy.Fsm.ChangeState(Enemy.OBSERVE_STATE);
            }
            else
            {
                enemy.NextPoint();
                m_SubPath = enemy.GenerateSubPath(enemy.GetCurrentPoint().Node);
            }
        }

        public void OnExit(Enemy enemy)
        {

        }

        public void OnDrawGizmos(Enemy enemy)
        {
            Gizmos.color = Color.red;
            foreach (var subPath in m_SubPath)
            {
                Gizmos.DrawWireSphere(subPath, 0.2f);
            }
        }
        #endregion
    }
}

