using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Player : MonoBehaviour 
    {
        [Space]
        public float moveSpeed = 2.5f;

        public float Radius
        {
            get
            {
                return m_Collider.radius;
            }
        }

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

        private CircleCollider2D m_Collider;

        void Awake ()
        {
            m_Collider = GetComponent<CircleCollider2D>();
        }

        void Update ()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical);
            Vector2 velocity = direction * moveSpeed * Time.deltaTime;

            Position = Position + velocity;
        }
    }

}