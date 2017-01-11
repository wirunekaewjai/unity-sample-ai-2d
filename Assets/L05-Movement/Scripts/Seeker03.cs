using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Seeker03 : MonoBehaviour 
    {
        public bool drawGizmos = true;

        [Space]
        public Player player;

        public float moveSpeed = 2f;
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

        public Quaternion Rotation
        {
            get
            {
                return transform.rotation;
            }
            set
            {
                transform.rotation = value;
            }
        }

        void Update ()
        {
            // Move
            Vector2 displacement = player.Position - Position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = direction * moveSpeed * Time.deltaTime;

            float remainingDistance = Vector2.Distance(player.Position, Position);
            if (remainingDistance >= player.radius + radius)
            {
                Position = Position + velocity;
            }

            // Rotate
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            Rotation = Quaternion.RotateTowards(Rotation, lookAt, rotateSpeed);
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

}