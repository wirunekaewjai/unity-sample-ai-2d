using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Raycast' for Hiding
    public class Seeker06 : MonoBehaviour 
    {
        public Transform target;

        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 1f;
        public float maxDistance = 3f;

        public float fleeDistance = 0.5f;

        void Update ()
        {
            float moveStep = moveSpeed * Time.deltaTime;
            float rotateStep = rotateSpeed * Time.deltaTime;

            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = Vector2.up * moveStep;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);

            if (hit.transform == target)
            {
                if (hit.distance >= minDistance)
                {
                    Vector3 newDirection = Vector3.RotateTowards(transform.up, direction, rotateStep, 0f);

                    transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
                    transform.Translate(velocity);
                }
                else if(hit.distance <= fleeDistance)
                {
                    Vector3 newDirection = Vector3.RotateTowards(transform.up, -direction, rotateStep, 0f);

                    transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
                    transform.Translate(velocity);
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
    }

}