using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Seeker07 : MonoBehaviour 
    {
        public Transform target;

        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 1f;
        public float maxDistance = 3f;

        public float fleeDistance = 0.5f;

        void Update ()
        {
            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);

            if (IsTargetInRange())
            {
                if (hit.distance > fleeDistance)
                {
                    RotateTo(direction);
                    MoveForward();
                }
                else
                {
                    RotateTo(-direction);
                    MoveForward();
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, minDistance);
            Gizmos.DrawWireSphere(transform.position, maxDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fleeDistance);
        }

        public bool IsTargetInRange()
        {
            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);
            return hit.transform == target && hit.distance >= minDistance;
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
    }

}