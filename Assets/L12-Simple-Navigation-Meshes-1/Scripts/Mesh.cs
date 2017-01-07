using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public class Mesh : MonoBehaviour
    {
        [SerializeField]
        private List<Edge> m_Edges = new List<Edge>();

        [SerializeField]
        private List<Mesh> m_Neighbors = new List<Mesh>();

        public List<Edge> Edges
        {
            get { return m_Edges; }
        }

        public List<Mesh> Neighbors
        {
            get { return m_Neighbors; }
        }

        public Vector2 Center
        {
            get
            {
                Vector2 sum = new Vector2();

                foreach (Edge edge in m_Edges)
                {
                    sum += edge.Center;
                }

                return sum / m_Edges.Count;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(Center, 0.075f);

            foreach (Edge edge in m_Edges)
            {
                edge.DrawGizmos(Color.cyan);
            }
        }
    }
}