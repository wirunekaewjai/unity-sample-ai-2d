using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L08
{
    public class ObserveState : Wirune.L04.IState<Agent>
    {
        private int m_CurrentIndex;
        private int m_CurrentCount;
        private int m_MaxCount;

        private float m_EndTime;

        private Vector2 m_CurrentDirection;
        private Vector2[] m_Directions = 
            {
                new Vector2(-1, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(0, -1),
            };

        #region IState implementation
        public void OnEnter(Agent agent)
        {
            m_CurrentIndex = Random.Range(0, m_Directions.Length);
            m_CurrentCount = 0;
            m_MaxCount = Random.Range(1, 5);
            m_EndTime = Time.time + Random.Range(0.5f, 2f);
            m_CurrentDirection = agent.transform.up;
        }

        public void OnStay(Agent agent)
        {
            if (agent.HasPlayer())
            {
                agent.GetFsm().ChangeState(Agent.SEEK_STATE);
            }
            else
            {
                if (Time.time >= m_EndTime)
                {
                    m_CurrentCount++;

                    if (m_CurrentCount >= m_MaxCount)
                    {
                        agent.GetFsm().ChangeState(Agent.PATROL_STATE);

                        return;
                    }

                    m_CurrentIndex = Random.Range(0, m_Directions.Length);
                    m_CurrentDirection = m_Directions[m_CurrentIndex];

                    m_EndTime = Time.time + Random.Range(1f, 5f);
                }

                agent.RotateTo(m_CurrentDirection);
            }
        }

        public void OnExit(Agent agent)
        {
            
        }
        #endregion
    }
}

