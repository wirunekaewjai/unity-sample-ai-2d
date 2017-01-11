using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L09
{
    using IState = Wirune.L04.IState<Agent>;

    public class ObserveState : IState
    {
        // Non-Serialized
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

        public void OnExit(Agent agent)
        {

        }
        #endregion
    }
}

