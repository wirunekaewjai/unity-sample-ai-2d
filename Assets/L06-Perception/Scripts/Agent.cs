using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Agent : MonoBehaviour
    {
        private CircleCollider2D m_Player;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private float m_StopDistance = 0.75f;

        [SerializeField]
        private float m_FleeDistance = 0.5f;

        void OnValidate()
        {
            m_StopDistance = Mathf.Clamp(m_StopDistance, 0.1f, 100f);
            m_FleeDistance = Mathf.Clamp(m_FleeDistance, 0.075f, m_StopDistance);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_StopDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_FleeDistance);
        }

        void Update ()
        {
            if (null != m_Player)
            {
                Vector2 displacement = m_Player.transform.position - transform.position;
                float distance = displacement.magnitude - m_Player.radius;

                if (distance >= m_StopDistance)
                {
                    RotateTo(displacement);
                    MoveForward();
                }
                else if (distance <= m_FleeDistance)
                {
                    RotateTo(-displacement);
                    MoveForward();
                }
            }
        }

        public void RotateTo(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, m_RotateSpeed);
        }

        public void MoveForward()
        {
            float moveSpeedPerFrame = m_MoveSpeed * Time.deltaTime;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }

        public void OnPlayerEnter(Collider2D player)
        {
            m_Player = player.GetComponent<CircleCollider2D>();
        }

        public void OnPlayerExit(Collider2D player)
        {
            m_Player = null;
        }
    }
}