using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Look At' Code
    public class Seeker02 : MonoBehaviour 
    {
        [SerializeField]
        private Transform m_Player;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        void Update ()
        {
            Vector2 displacement = m_Player.position - transform.position;

            RotateTo(displacement);
            MoveForward();
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