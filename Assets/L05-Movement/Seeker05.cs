using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Flee'
    public class Seeker05 : MonoBehaviour 
    {
        [SerializeField]
        private Player m_Player;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private float m_MinDistance = 1f;

        [SerializeField]
        private float m_MaxDistance = 3f;

        [SerializeField]
        private float m_FleeDistance = 0.5f;

        void OnValidate()
        {
            m_MaxDistance = Mathf.Clamp(m_MaxDistance, 0.15f, 100f);
            m_MinDistance = Mathf.Clamp(m_MinDistance, 0.1f, m_MaxDistance);
            m_FleeDistance = Mathf.Clamp(m_FleeDistance, 0.075f, m_MinDistance);
        }

        void Update ()
        {
            Vector2 displacement = m_Player.transform.position - transform.position;
            float distance = displacement.magnitude - m_Player.radius;

            if (distance <= m_MaxDistance)
            {
                if (distance >= m_MinDistance)
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_MinDistance);
            Gizmos.DrawWireSphere(transform.position, m_MaxDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_FleeDistance);
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateSpeedPerFrame = m_RotateSpeed * Time.deltaTime;
            Vector2 newDirection = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);

            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        }

        public void MoveForward()
        {
            float moveSpeedPerFrame = m_MoveSpeed * Time.deltaTime;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }
    }

}