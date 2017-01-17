using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class GraphAgent : MonoBehaviour
    {
        public bool drawGizmos = true;

        [Space]
        public Heuristic heuristic = Heuristic.Euclidean;

        [Space]
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        [Space]
        public float radius = 0.5f;

        [Space]
        public float stoppingDistance = 0.02f;


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

        public Vector2 Destination
        {
            get
            {
                return m_Destination;
            }
            set
            {
                if (Vector2.Distance(m_Destination, value) >= Mathf.Epsilon)
                {
                    IsReached = false;

                    m_Destination = value;
                    CalculatePath();
                }
                else if (Vector2.Distance(Position, value) <= stoppingDistance)
                {
                    IsReached = true;
                }
            }
        }

        public bool IsReached { get; private set; }

        // Non-Serialized
        private List<Vector2> m_Path = new List<Vector2>();
        private Vector2 m_Destination;

        void Awake ()
        {
            Destination = transform.position;
        }

        void FixedUpdate()
        {
            if (m_Path.Count > 0)
            {
                IsReached = false;

                Vector2 position = Position;
                Vector2 target = m_Path[0];
                Vector2 velocity = Seek(target);

                float remainingDistance = Vector2.Distance(target, position);
                if (remainingDistance > stoppingDistance)
                {
                    Position = position + velocity;
                }
                else
                {
                    Position = target;

                    m_Path.RemoveAt(0);
                    IsReached = (m_Path.Count == 0);
                }

                Rotate(velocity);
            }
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            if (m_Path.Count > 1)
            {
                Gizmos.color = Color.yellow;

                for (int i = 0; i < m_Path.Count - 1; i++)
                {
                    Gizmos.DrawLine(m_Path[i], m_Path[i + 1]);
                }

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, m_Path[0]);
            }
            else if (m_Path.Count == 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, m_Path[0]);
            }

        }

        public Vector2 Seek(Vector2 target)
        {
            Vector2 displacement = (target - Position);
            Vector2 direction = displacement.normalized;

            return direction * moveSpeed * Time.fixedDeltaTime;
        }

        public void Rotate(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeed);
        }

        public void Move(Vector2 direction)
        {
            Vector2 velocity = direction * moveSpeed * Time.deltaTime;
            Vector2 next = Position + velocity;

            Graph graph = FindObjectOfType<Graph>();
            List<Node> nodes = graph.Nodes;
           
            Node a = Node.FindNearest(nodes, Position);

            if (!a.Contains(next))
            {
                Node b = Node.FindNearest(nodes, next, a);

                if (null == b)
                {
                    next = a.ClosestPoint(next);
                }
            }

            Position = next;
        }

        public void CalculatePath()
        {
            m_Path = Graph.Search(Position, m_Destination, heuristic, radius);
        }
    }
}

