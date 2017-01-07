using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Max Distance'
    public class Seeker03 : MonoBehaviour 
    {
        [SerializeField]
        private Player m_Player;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private float m_MaxDistance = 3f;

        void Update ()
        {
            Vector2 displacement = m_Player.transform.position - transform.position;
            float distance = displacement.magnitude - m_Player.radius;

            if (distance <= m_MaxDistance)
            {
                RotateTo(displacement);
                MoveForward();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_MaxDistance);
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
    }

}