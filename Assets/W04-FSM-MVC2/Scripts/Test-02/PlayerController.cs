using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W04.Test02
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerModel m_Model;
        [SerializeField] private PlayerView m_PlayerView;
        [SerializeField] private HUDView m_HUDView;

        private void Start()
        {
            m_PlayerView.HitEvent += OnHit;

            m_Model.HealthChangedEvent += m_HUDView.UpdateHealth;
            m_Model.ScoreChangedEvent += m_HUDView.UpdateScore;
            m_Model.DiedEvent += OnDied;
            m_Model.Start();
        }

        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            var velocity = new Vector2(h, v) * m_Model.Speed;

            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            m_PlayerView.UpdateVelocity(velocity);
            m_PlayerView.UpdateLookAt(mouseWorldPosition);

            if (Input.GetMouseButtonDown(0))
            {
                var forward = Camera.main.transform.forward;
                var hit = Physics2D.Raycast(mouseWorldPosition, forward);
                var collider = hit.collider;

                if (null != collider && collider.tag == "Respawn")
                {
                    collider.GetComponent<EnemyView>().Hit();
                    OnKill();
                }
            }
        }

        private void OnHit()
        {
            m_Model.Health -= 1;
        }

        private void OnKill()
        {
            m_Model.Score += 1;
        }

        private void OnDied()
        {
            Debug.Log("Died");
        }
    }
}
