using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using IState = Wirune.L04.IState<Agent>;
    using ObservablePoint = Wirune.L09.ObservablePoint;

    public class ObserveState : IState
    {
        private ObservablePoint m_Point;

        private int m_CurrentIndex;
        private float m_EndTime;

        #region IState implementation
        public void OnEnter(Agent agent)
        {
            m_Point = agent.GetCurrentPoint() as ObservablePoint;

            m_CurrentIndex = 0;
            m_EndTime = Time.time + m_Point.Duration;
        }

        public void OnStay(Agent agent)
        {
            if (null != agent.Player)
            {
                agent.Fsm.ChangeState(Agent.CHASE_STATE);
            }
            else
            {   
                if (Time.time >= m_EndTime)
                {
                    m_CurrentIndex++;

                    if (m_CurrentIndex >= m_Point.Count)
                    {
                        agent.NextPoint();
                        agent.Fsm.ChangeState(Agent.PATROL_STATE);

                        return;
                    }

                    m_EndTime = Time.time + m_Point.Duration;
                }

                agent.Rotate(m_Point.GetDirection(m_CurrentIndex));
            }
        }

        public void OnExit(Agent agent)
        {

        }
        #endregion
    }
}

