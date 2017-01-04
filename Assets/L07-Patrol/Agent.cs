using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L07
{
    public class Agent : MonoBehaviour 
    {
        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private Path m_Path;

        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        void Update()
        {
            Vector2 currentTarget = GetCurrentPoint();
            Vector2 currentPosition = transform.position;

            Vector2 displacement = currentTarget - currentPosition;
            float distance = displacement.magnitude;

            if (distance > 0.05f)
            {
                RotateTo(displacement);
                MoveForward(distance);
            }
            else
            {
                NextPoint();
            }
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateSpeedPerFrame = m_RotateSpeed * Time.deltaTime;
            Vector2 newDirection = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);

            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        }

        public void MoveForward(float distance)
        {
            float moveSpeedPerFrame = Mathf.Min(m_MoveSpeed * Time.deltaTime, distance);
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }

        public Vector2 GetCurrentPoint()
        {
            return m_Path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            if (m_IsForward)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= m_Path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = m_Path.Count - 1;
                }
            }
            else
            {
                m_CurrentPointIndex--;

                if (m_CurrentPointIndex < 0)
                {
                    m_IsForward = true;
                    m_CurrentPointIndex = 0;
                }
            }
        }
    }
}