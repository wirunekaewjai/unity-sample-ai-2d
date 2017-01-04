using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Follower : MonoBehaviour
    {
        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private Path m_Path;

        private int m_CurrentPointIndex = 0;

        void Update()
        {
            if (!IsPathEnded())
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

        public bool IsPathEnded()
        {
            return m_CurrentPointIndex >= m_Path.Count;
        }

        public Vector2 GetCurrentPoint()
        {
            return m_Path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            m_CurrentPointIndex++;
        }
    }
}

