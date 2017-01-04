using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Follower : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 0.25f;

        public Path path;
        private int m_CurrentPointIndex = 0;

        void Update()
        {
            if (m_CurrentPointIndex >= path.Count)
            {
                return;
            }

            Vector2 currentPoint = GetCurrentPoint();
            Vector2 currentPosition = transform.position;

            Vector2 displacement = currentPoint - currentPosition;
            Vector2 direction = displacement.normalized;

            RotateTo(direction);
            MoveForward();

            float distance = displacement.magnitude;
            if (distance < minDistance)
            {
                m_CurrentPointIndex++;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistance);
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateStep = rotateSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.up, direction, rotateStep, 0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        }

        public void MoveForward()
        {
            float moveStep = moveSpeed * Time.deltaTime;
            Vector2 velocity = Vector2.up * moveStep;

            transform.Translate(velocity);
        }

        public Vector2 GetCurrentPoint()
        {
            return path.GetPoint(m_CurrentPointIndex);
        }
    }
}

