using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03.Test02
{
    public class AgentModel : Model
    {
        [SerializeField]
        int m_MaxHealth = 20;

        [SerializeField]
        int m_Health = 10;

        protected override void Awake()
        {
            base.Awake();
            Execute("OnHealthChanged", m_Health, m_MaxHealth);
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
                Execute("OnHealthChanged", m_Health, m_MaxHealth);

                if (m_Health == 0)
                {
                    Debug.Log("Died");
                    Execute("OnDied");
                }
            }
        }

//        public void IncreaseHealth(int value)
//        {
//            m_Health = Mathf.Min(m_Health + value, m_MaxHealth);
//            Execute("OnHealthChanged", m_Health, m_MaxHealth);
//        }
//
//        public void DecreaseHealth(int value)
//        {
//            m_Health = Mathf.Max(m_Health - value, 0);
//            Execute("OnHealthChanged", m_Health, m_MaxHealth);
//
//            if (m_Health == 0)
//            {
//                Debug.Log("Died");
//                Execute("OnDied");
//            }
//        }
    }
}
