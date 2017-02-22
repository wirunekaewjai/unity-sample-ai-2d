using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test02
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyModel m_Model;
        [SerializeField] private EnemyView m_View;

        public EnemyModel Model { get { return m_Model; } }
        public EnemyView View { get { return m_View; } }

        private PlayerView m_PlayerView;

        private void Awake()
        {
            m_PlayerView = FindObjectOfType<PlayerView>();
        }

        private void Update()
        {
            if (null == m_PlayerView)
                return;

            m_View.OnSeek(m_PlayerView.Position, m_Model.Speed);
        }
    }
}
