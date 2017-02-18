using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test01
{
    public class AgentView : MonoBehaviour, IView
    {
        private static readonly float c_SmoothStep = 0.25f;

        public UnityAction<Collider> TriggerEnterEvent;

        [SerializeField]
        private Slider m_HealthSlider;
        private float m_CurrentHealth;

        public void Move(Vector3 velocity)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        public void SetMaxHealth(int maxHealth)
        {
            m_HealthSlider.maxValue = maxHealth;
        }

        public void SetHealth(int health)
        {
            m_CurrentHealth = health;
        }

        private void Update()
        {
            var value = m_HealthSlider.value;

            if (value < m_CurrentHealth)
            {
                value += c_SmoothStep;
            }
            else if (value > m_CurrentHealth)
            {
                value -= c_SmoothStep;
            }

            m_HealthSlider.value = value;
        }

        private void OnTriggerEnter(Collider c)
        {
            Dispatcher.Invoke(TriggerEnterEvent, c);
        }
    }
}
