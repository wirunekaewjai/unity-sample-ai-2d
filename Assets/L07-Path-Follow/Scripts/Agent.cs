using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L07
{
    public class Agent : MonoBehaviour
    {
        public bool drawGizmos = true;

        [Space]
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        [Space]
        public float stoppingDistance = 0.1f;
        public Path path;

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        // Non-Serialized
        private int m_CurrentPointIndex = 0;

        void Update()
        {
            if (!IsPathEnded())
            {
                Vector2 target = GetCurrentPoint().Position;
                Vector2 velocity = Seek(target);

                float remainingDistance = Vector2.Distance(target, Position);
                if (remainingDistance >= stoppingDistance)
                {
                    Position = Position + velocity;
                    Rotate(velocity);
                }
                else
                {
                    NextPoint();
                }
            }
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            if (!IsPathEnded())
            {
                Vector2 target = GetCurrentPoint().Position;

                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(Position, target);
            }
        }

        // Copied from L05-Movement
        public Vector2 Seek(Vector2 target)
        {
            Vector2 displacement = (target - Position);
            Vector2 direction = displacement.normalized;

            return direction * moveSpeed * Time.deltaTime;
        }

        public void Rotate(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeed);
        }
        //

        public bool IsPathEnded()
        {
            return m_CurrentPointIndex >= path.Count;
        }

        public Point GetCurrentPoint()
        {
            return path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            m_CurrentPointIndex++;
        }
    }
}

