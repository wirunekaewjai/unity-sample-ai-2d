using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    [System.Serializable]
    public class PlayerModel
    {
        public UnityAction<int> ScoreChangedEvent;
        public UnityAction<int> HealthChangedEvent;
        public UnityAction DiedEvent;

        [SerializeField, Range(0, 10)] private float m_Speed = 2f;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = Mathf.Clamp(value, 0f, 10f); }
        }

        private int m_Score;
        public int Score
        {
            get { return m_Score; }
            set
            { 
                m_Score = Mathf.Clamp(value, 0, 99999);
                Dispatcher.Invoke(ScoreChangedEvent, m_Score);
            }
        }

        private int m_Health;
        public int Health
        {
            get { return m_Health; }
            set
            { 
                m_Health = Mathf.Clamp(value, 0, 99);
                Dispatcher.Invoke(HealthChangedEvent, m_Health);

                if (m_Health == 0)
                {
                    Dispatcher.Invoke(DiedEvent);
                }
            }
        }

        public void Start()
        {
            Health = 10;
            Score = 0;
        }
    }
}
