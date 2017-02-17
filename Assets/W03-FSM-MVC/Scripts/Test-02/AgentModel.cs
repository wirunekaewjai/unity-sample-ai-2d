﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W03.Test02
{
    public class AgentModel : Model
    {
        [SerializeField]
        private int m_MaxHealth = 20;

        [SerializeField]
        private int m_Health = 10;

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
                Notify(AgentEvent.HealthChanged, m_Health);
            }
        }
    }
}
