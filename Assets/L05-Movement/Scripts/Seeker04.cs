using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Seeker04 : MonoBehaviour 
    {
        public bool drawGizmos = true;

        [Space]
        public Player player;

        public float moveSpeed = 2;
        public float rotateSpeed = 5f;

        public float radius = 0.5f;

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

        void Update ()
        {
            Vector2 velocity = Seek(player.Position);

            float remainingDistance = Vector2.Distance(player.Position, Position);
            if (remainingDistance >= player.radius + radius)
            {
                Position = Position + velocity;
            }

            Rotate(velocity);
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            if (null != player)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Position, player.Position);
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
    }

}