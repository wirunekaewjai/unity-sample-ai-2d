using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class ObserveState : IState<Enemy>
    {
        private ObservablePoint m_Point;

        private int m_CurrentIndex;
        private float m_EndTime;

        #region IState implementation
        public void OnEnter(Enemy enemy)
        {
            m_Point = enemy.GetCurrentPoint() as ObservablePoint;

            m_CurrentIndex = 0;
            m_EndTime = Time.time + m_Point.Duration;
        }

        public void OnStay(Enemy enemy)
        {
            if (null != enemy.Player)
            {
                enemy.Fsm.ChangeState(Enemy.CHASE_STATE);
            }
            else
            {   
                if (Time.time >= m_EndTime)
                {
                    m_CurrentIndex++;

                    if (m_CurrentIndex >= m_Point.Count)
                    {
                        enemy.NextPoint();
                        enemy.Fsm.ChangeState(Enemy.PATROL_STATE);

                        return;
                    }

                    m_EndTime = Time.time + m_Point.Duration;
                }

                enemy.agent.Rotate(m_Point.GetDirection(m_CurrentIndex));

            }
        }

        public void OnExit(Enemy enemy)
        {

        }
        #endregion
    }
}

