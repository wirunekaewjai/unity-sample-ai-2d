using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test02
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyModel m_Model;
        [SerializeField] private EnemyView m_View;

        private PlayerView m_PlayerView;

        private void Start()
        {
            m_PlayerView = FindObjectOfType<PlayerView>();
            m_View.HitEvent += OnHit;
        }

        private void Update()
        {
            if (null == m_PlayerView)
                return;

            m_View.Move(m_PlayerView.Position, m_Model.Speed);
        }

        private void OnHit()
        {
            EnemyManager.Instance.Despawn(gameObject);
        }
    }
}
