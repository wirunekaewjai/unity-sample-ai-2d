using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test03
{
    public class AgentView : View
    {
        [SerializeField]
        private float m_Speed = 2f;

        [SerializeField]
        private Slider m_HealthSlider;

        public float Speed
        {
            get { return m_Speed; }
        }

        public Fsm<AgentView> Fsm { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Fsm = new Fsm<AgentView>(this);
            Fsm.AddState(1, new AgentViewState1());
            Fsm.AddState(2, new AgentViewState2());

            Fsm.ChangeState(1);
        }

        public override void OnNotify(object eventID, params object[] parameters)
        {
            base.OnNotify(eventID, parameters);
            Fsm.Notify(eventID, parameters);
        }

        void OnTriggerEnter(Collider c)
        {
            Notify(AgentEvent.Collect, c);
        }

        [Observe(AgentEvent.MaxHealthChanged)]
        void OnMaxHealthChanged(int maxHealth)
        {
            m_HealthSlider.maxValue = maxHealth;
        }

        [Observe(AgentEvent.HealthChanged)]
        void OnHealthChanged(int health)
        {
            m_HealthSlider.value = health;
        }

        [Observe(AgentEvent.Died)]
        void OnDied()
        {
            Debug.Log("Died");
            Fsm.ChangeState(2);
        }
    }
}
