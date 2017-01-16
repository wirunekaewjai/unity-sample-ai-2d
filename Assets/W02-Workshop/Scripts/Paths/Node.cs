using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W02
{
    [System.Serializable]
    public class Node
    {
        [SerializeField]
        private Vector2 m_Position;

        [SerializeField]
        private Vector2 m_Size;

        [SerializeField]
        private Bounds m_Bounds = new Bounds();

        [SerializeField]
        private List<int> m_Neighbors = new List<int>();

        public Node()
        {
            Position = Vector2.zero;
            Size = Vector2.zero;
        }

        public Node(Vector2 position)
        {
            Position = position;
            Size = Vector2.zero;
        }

        public Node(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public Vector2 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                if (value != m_Position)
                {
                    m_Position = value;
                    m_Bounds = new Bounds(m_Position, m_Size);
                }
            }
        }

        public Vector2 Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                if (value != m_Size)
                {
                    m_Size = value;
                    m_Bounds = new Bounds(m_Position, m_Size);
                }
            }
        }

        public Bounds Bounds
        {
            get
            {
                return m_Bounds;
            }
        }


        public int GetNeighborCount()
        {
            return m_Neighbors.Count;
        }

        public int GetNeighbor(int neighborIndex)
        {
            return m_Neighbors[neighborIndex];
        }

        public void AddNeighbor(int nodeIndex)
        {
            m_Neighbors.Add(nodeIndex);
        }

        public void RemoveNeighbor(int nodeIndex)
        {
            m_Neighbors.Remove(nodeIndex);
        }

        public Vector2 ClosestPoint(Vector2 point)
        {
            return Bounds.ClosestPoint(point);
        }

        public bool Contains(Vector2 point)
        {
            return Bounds.Contains(point);
        }

        public bool Intersects(Node node)
        {
            return Bounds.Intersects(node.Bounds);
        }

        public bool IntersectRay(Ray ray)
        {
            return Bounds.IntersectRay(ray);
        }

        public bool IntersectRay(Ray ray, out float distance)
        {
            return Bounds.IntersectRay(ray, out distance);
        }

        public void Encapsulate(Node node)
        {
            m_Bounds.Encapsulate(node.Bounds);
            m_Position = m_Bounds.center;
            m_Size = m_Bounds.size;
        }

        public static Node FindNearest(List<Node> nodes, Vector2 position)
        {
            foreach (var node in nodes)
            {
                if (node.Contains(position))
                    return node;
            }

            IOrderedEnumerable<Node> ordereds = (from n in nodes
                orderby (n.ClosestPoint(position) - position).sqrMagnitude ascending
                select n);

            return ordereds.First();
        }

        public static Node FindNearest(List<Node> nodes, Vector2 position, params Node[] excludes)
        {
            foreach (var node in nodes)
            {
                if (node.Contains(position))
                    return node;
            }

            IOrderedEnumerable<Node> ordereds = (from n in nodes
                orderby (n.ClosestPoint(position) - position).sqrMagnitude ascending
                select n);

            Node first = ordereds.First();

            if (excludes.Contains(first))
                return null;

            return first;
        }
    }
}

