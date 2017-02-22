using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test01
{
    public class AgentController : MonoBehaviour
    {
        [SerializeField] private AgentModel m_Model;
        [SerializeField] private AgentView m_View;

        public Fsm<AgentController> Fsm { get; private set; }
        public AgentModel Model { get { return m_Model; } }
        public AgentView View { get { return m_View; } }

        private void Awake()
        {
            Fsm = new Fsm<AgentController>(this);

            Fsm.CreateState<AgentControllerState1>(1);
            Fsm.CreateState<AgentControllerState2>(2);

            Fsm.ChangeState(1);
        }

        private void Start()
        {
            Model.MaxHealthChangedEvent += View.SetMaxHealth;
            Model.HealthChangedEvent += View.SetHealth;
            Model.Start();
        }
    }
}
