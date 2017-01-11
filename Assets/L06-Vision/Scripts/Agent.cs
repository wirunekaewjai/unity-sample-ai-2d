using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Agent : MonoBehaviour
    {
        public bool drawGizmos = true;

        [Space]
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float radius = 0.5f;

        [Space]
        public EyeVision eyeVision;

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

        private Player m_Player;

        void Awake ()
        {
            eyeVision.onEnter.AddListener(OnObjectInSight);
            eyeVision.onExit.AddListener(OnObjectOutSight);
        }

        void Update ()
        {
            if (null != m_Player)
            {
                Vector2 velocity = Seek(m_Player.Position);

                float remainingDistance = Vector2.Distance(m_Player.Position, Position);
                if (remainingDistance >= m_Player.Radius + radius)
                {
                    Position = Position + velocity;
                }

                Rotate(velocity);
            }
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            if (null != m_Player)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Position, m_Player.Position);
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(Position, transform.up);

        }

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

        public void OnObjectInSight(Collider2D c)
        {
            if (c.tag == "Player")
            {
                m_Player = c.GetComponent<Player>();
            }
        }

        public void OnObjectOutSight(Collider2D c)
        {
            if (c.tag == "Player")
            {
                m_Player = null;
            }
        }
    }
}