using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test02
{
    public class TestFSM : MonoBehaviour 
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
                OnHealthChanged(m_Health);
            }
        }

        public float speed = 2f;
        public Slider healthSlider;

        public Fsm<TestFSM> Fsm { get; private set; }

        private void Awake()
        {
            Fsm = new Fsm<TestFSM>(this);
            Fsm.AddState(1, new TestState1());
            Fsm.AddState(2, new TestState2());
            Fsm.ChangeState(1);
        }

        private void OnEnable()
        {
            OnMaxHealthChanged(MaxHealth);
            OnHealthChanged(Health);
        }

        private void OnTriggerEnter(Collider c)
        {
            Fsm.Notify("Collect", c);
        }

        private void OnMaxHealthChanged(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
        }

        private void OnHealthChanged(int health)
        {
            healthSlider.value = health;
        }
    }
}
