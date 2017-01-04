﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Raycast' for Collider Checking
    public class Seeker05 : MonoBehaviour 
    {
        public Transform target;

        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 1f;
        public float maxDistance = 3f;

        void Update ()
        {
            float moveSpeedPerFrame = moveSpeed * Time.deltaTime;
            float rotateSpeedPerFrame = rotateSpeed * Time.deltaTime;

            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);

            if (hit.transform == target && hit.distance >= minDistance)
            {
                transform.up = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);
                transform.Translate(velocity);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, minDistance);
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }

}