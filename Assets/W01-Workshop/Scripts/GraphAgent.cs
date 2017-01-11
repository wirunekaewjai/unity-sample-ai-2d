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

        void Update ()
        {
            if (m_Path.Count > 0)
            {
                IsReached = false;

                Vector2 position = Position;
                Vector2 target = m_Path[0];
                Vector2 velocity = Seek(target);

                float remainingDistance = Vector2.Distance(target, position);
                if (remainingDistance >= stoppingDistance)
                {
                    Position = position + velocity;
                }
                else
                {
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
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, m_Path[0]);

                Gizmos.color = Color.cyan;

                for (int i = 0; i < m_Path.Count - 1; i++)
                {
                    Gizmos.DrawLine(m_Path[i], m_Path[i + 1]);
                }
            }
        }

        public Vector2 Seek(Vector2 target)
        {
            Vector2 displacement = (target - Position);
            Vector2 direction = displacement.normalized;

            return direction * moveSpeed * Time.deltaTime;
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

            Node goal = graph.FindNearest(m_Destination);

            if (IsInside(goal, m_Destination))
            {
                m_Path.Add(m_Destination);
                return;
            }

            Node start = graph.FindNearest(Position);

            List<Node> nodes = AStar.Search(start, goal, heuristic);
            nodes.Remove(start);

            PathSmoother.Smooth(nodes, 0.5f);

            foreach (var node in nodes)
            {
                m_Path.Add(node.position);
            }

            Vector2 closest = ClosestPoint(goal, m_Destination);
            m_Path.Add(closest);
        }


        private Vector2 ClosestPoint(Node node, Vector2 point)
        {
            Bounds b = new Bounds(node.position, node.size);

            Vector2 disp = (m_Destination - node.position);
            Vector2 closest = b.ClosestPoint(node.position + disp.normalized * 1000f);

            float d1 = disp.sqrMagnitude;
            float d2 = (closest - node.position).sqrMagnitude;

            if (d1 >= d2)
            {
                return closest;
            }

            return point;
        }

        private bool IsInside(Node node, Vector2 point)
        {
            Bounds b = new Bounds(node.position, node.size);

            Vector2 disp = (m_Destination - node.position);
            Vector2 closest = b.ClosestPoint(node.position + disp.normalized * 1000f);

            float d1 = disp.sqrMagnitude;
            float d2 = (closest - node.position).sqrMagnitude;

            return (d1 < d2);
        }

    }
}

