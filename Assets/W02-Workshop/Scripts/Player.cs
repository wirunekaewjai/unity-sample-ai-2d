using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class Player : MonoBehaviour 
    {
        public GraphAgent agent;

        void FixedUpdate ()
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                Vector2 position = transform.position;
                Vector2 direction = new Vector2(horizontal, vertical).normalized;
//                Vector2 destination = position + (direction * agent.moveSpeed * Time.deltaTime);
//
//                agent.Destination = destination;

                agent.Move(direction);
                agent.Rotate(direction);
            }
        }
    }
}

