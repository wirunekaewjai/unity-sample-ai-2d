using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
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
        private readonly List<Vector2> m_Path = new List<Vector2>();
        private Vector2 m_Destination;

        void Awake ()
        {
            Destination = transform.position;
        }

        void FixedUpdate ()
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
        }

        public Vector2 Seek(Vector2 target)
        {
            Vector2 displacement = (target - Position);
            Vector2 direction = displacement.normalized;

            return direction * moveSpeed * Time.deltaTime;
        }

        public Vector2 Truncate(Vector2 velocity, float distance)
        {
            if (velocity.magnitude > distance)
            {
                velocity = velocity.normalized * distance;
            }

            return velocity;
        }

        public void Rotate(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeed);
        }

        public void CalculatePath()
        {
            m_Path.Clear();

            GridGraph graph = Object.FindObjectOfType<GridGraph>();

            Node start = graph.FindNearest(Position);
            Node goal = graph.FindNearest(m_Destination);

            if (start == goal || start.neighbors.Contains(goal))
            {
                if (IsInside(goal, m_Destination))
                {
                    m_Path.Add(m_Destination);
                }
                else
                {
                    m_Destination = ClosestPoint(goal, m_Destination);
                    m_Path.Add(m_Destination);
                }

                return;
            }

            List<Node> nodes = AStar.Search(start, goal, heuristic);
            nodes.Remove(start);

            PathSmoother.Smooth(nodes, 0.5f);

            foreach (var node in nodes)
            {
                m_Path.Add(node.position);
            }

            if (IsInside(goal, m_Destination))
            {
                m_Destination = ClosestPoint(goal, m_Destination);
                m_Path.Add(m_Destination);
            }
        }


        private Vector2 ClosestPoint(Node node, Vector2 point)
        {
            Bounds b = new Bounds(node.position, node.size);
            float size = node.size.magnitude;

            Vector2 disp = (point - Position);
            Vector2 closest = b.ClosestPoint(Position + (disp.normalized * size));

            float d1 = disp.sqrMagnitude;
            float d2 = (closest - Position).sqrMagnitude;

            if (d1 >= d2)
            {
                return closest;
            }

            return point;
        }

        private bool IsInside(Node node, Vector2 point)
        {
            Bounds b = new Bounds(node.position, node.size);
            float size = node.size.magnitude;

            Vector2 disp = (point - Position);
            Vector2 closest = b.ClosestPoint(Position + (disp.normalized * size));

            float d1 = disp.sqrMagnitude;
            float d2 = (closest - Position).sqrMagnitude;

            return (d1 < d2);
        }

    }
}

