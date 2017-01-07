using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Seeker01 : MonoBehaviour 
    {
        public Transform target;
        public float speed = 2f;

        void Update ()
        {
            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;
            Vector2 velocity = direction * speed * Time.deltaTime;

            transform.Translate(velocity);
        }
    }

}