using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class Player : MonoBehaviour 
    {
        public GraphAgent agent;
        public CircleCollider2D collider;

        public float Radius
        {
            get
            {
                return collider.radius;
            }
        }

        void FixedUpdate ()
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                Vector2 position = transform.position;
                Vector2 direction = new Vector2(horizontal, vertical).normalized;
                Vector2 destination = position + (direction * agent.moveSpeed * Time.fixedDeltaTime);

                agent.Destination = destination;
            }
        }

//        [SerializeField]
//        private CircleCollider2D m_Collider;
//
//        private Node m_Node;
//
//        public float Radius
//        {
//            get
//            {
//                Vector2 scale = transform.localScale;
//                return Mathf.Max(scale.x, scale.y) * m_Collider.radius;
//            }
//        }
//
//        public Node Node
//        {
//            get
//            {
//                return m_Node;
//            }
//        }
//
//        protected virtual void Awake()
//        {
//            m_Node = FindObjectOfType<GridGraph>().FindNearest(transform.position);
//        }
//
//        protected virtual void Update()
//        {
//            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
//            {
//                float horizontal = Input.GetAxisRaw("Horizontal");
//                float vertical = Input.GetAxisRaw("Vertical");
//
//                Vector2 direction = new Vector2(horizontal, vertical).normalized;
//
//                RotateTo(direction);
//                MoveTo(direction, MoveSpeed);
//
//                UpdateNode();
//            }
//        }
//
//        private void UpdateNode()
//        {
//            Node nearest = FindNearestNode();
//            if (nearest != m_Node && nearest.walkable)
//            {
//                m_Node = nearest;
//            }
//            else
//            {
//                ClampPosition();
//            }
//        }
//
//        private void ClampPosition()
//        {
//            Bounds b = new Bounds(m_Node.position, m_Node.size);
//            Vector2 closet = b.ClosestPoint(transform.position);
//            Vector2 current = transform.position;
//            Vector2 displacement = closet - current;
//
//            transform.Translate(displacement * 1.01f, Space.World);
//        }
//
//        private Node FindNearestNode()
//        {
//            Vector2 position = transform.position;
//
//            Node nearest = m_Node;
//            float min = (m_Node.position - position).sqrMagnitude;
//
//            foreach (var neighbor in m_Node.neighbors)
//            {
//                float sqr = (neighbor.position - position).sqrMagnitude;
//
//                if (sqr < min)
//                {
//                    nearest = neighbor;
//                    min = sqr;
//                }
//            }
//
//            return nearest;
//        }
    }
}

