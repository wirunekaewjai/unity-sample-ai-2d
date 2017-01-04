using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L07
{
    public class Agent : MonoBehaviour 
    {
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 0.25f;

        public Path path;

        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        void Update()
        {
            float moveSpeedPerFrame = moveSpeed * Time.deltaTime;
            float rotateSpeedPerFrame = rotateSpeed * Time.deltaTime;

            Vector2 currentPoint = path.GetPoint(m_CurrentPointIndex);
            Vector2 currentPosition = transform.position;
            Vector2 displacement = currentPoint - currentPosition;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.up = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);
            transform.Translate(velocity);

            float distance = displacement.magnitude;

            if (distance < minDistance)
            {
                if (m_IsForward)
                {
                    m_CurrentPointIndex++;

                    if (m_CurrentPointIndex >= path.Count)
                    {
                        m_IsForward = false;
                        m_CurrentPointIndex = path.Count - 1;
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistance);
        }
    }
}