using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    // Add 'Look At' Code
    public class Seeker02 : MonoBehaviour 
    {
        public Transform target;

        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        void Update ()
        {
            float moveSpeedPerFrame = moveSpeed * Time.deltaTime;
            float rotateSpeedPerFrame = rotateSpeed * Time.deltaTime;

            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.up = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);
            transform.Translate(velocity);
        }
    }

}