﻿using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04.Test01
{
    [System.Serializable]
    public class AgentModel : IModel
    {
        public UnityAction<int> MaxHealthChangedEvent;
        public UnityAction<int> HealthChangedEvent;

        public UnityAction DiedEvent;
        public UnityAction ResurrectEvent;

        [SerializeField] private float m_Speed = 2f;
        [SerializeField] private int m_MaxHealth = 20;
        private int m_Health = 10;

        public float Speed
        {
            get
            {
                return m_Speed;
            }
        }

        public int MaxHealth
        {
            get
            {
                return m_MaxHealth;
            }
        }

        public int Health
        {
            get
            {
                return m_Health;
            }
            set
            {
                m_Health = Mathf.Clamp(value, 0, m_MaxHealth);
                HealthChangedEvent.Invoke(m_Health);

                if (m_Health == 0)
                {
                    DiedEvent.Invoke();
                }
            }
        }

        public void OnLoad()
        {
            MaxHealthChangedEvent.Invoke(MaxHealth);
            HealthChangedEvent.Invoke(Health);
        }

        public void Resurrect()
        {
            Health = MaxHealth;
            ResurrectEvent.Invoke();
        }
    }
}
