using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W03.Test02
{
    public class PlayerModel : MonoBehaviour
    {
        public UnityAction<int> HealthChangedEvent;
        public UnityAction DiedEvent;

        [SerializeField, Range(0, 10)] private float m_Speed = 2f;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = Mathf.Clamp(value, 0f, 10f); }
        }

        private int m_Health = 3;
        public int Health
        {
            get { return m_Health; }
            set
            { 
                m_Health = Mathf.Clamp(value, 0, 10);
                Dispatcher.Invoke(HealthChangedEvent, m_Health);

                if (m_Health == 0)
                {
                    Dispatcher.Invoke(DiedEvent);
                }
            }
        }

    }
}
