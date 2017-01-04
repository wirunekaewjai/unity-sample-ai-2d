using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Follower02 : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public bool isLoop;
        public Path02 path;
        public float minDistance = 0.25f;

        private int m_CurrentPointIndex = 0;

        void Update()
        {
            if (m_CurrentPointIndex >= path.Count)
            {
                if (isLoop)
                {
                    m_CurrentPointIndex -= path.Count;
                }
                else
                {
                    return;
                }
            }

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
                m_CurrentPointIndex++;
            }
        }
    }
}

