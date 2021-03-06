﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L05
{
    public class Player : MonoBehaviour 
    {
        public bool drawGizmos = true;

        public float moveSpeed = 2.5f;
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
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical);
            Vector2 velocity = direction * moveSpeed * Time.deltaTime;

            Position = Position + velocity;
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