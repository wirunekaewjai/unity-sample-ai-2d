using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L13
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private Mesh m_Mesh;

        [SerializeField]
        private List<Node> m_Neighbors = new List<Node>();

        public Mesh Mesh
        {
            get 
            {
                return m_Mesh;
            }
        }

        public Node this[int index]
        {
            get 
            {
                return m_Neighbors[index];
            }
        }

        public int Count
        {
            get
            {
                return m_Neighbors.Count;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector2 p0 = transform.position;
            Gizmos.DrawWireSphere(p0, 0.1f);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0.5f, 0);

            Vector2 p0 = Mesh.Center;
            foreach (var neighbor in m_Neighbors)
            {
                Vector2 p1 = neighbor.Mesh.Center;
                Gizmos.DrawLine(p0, p1);
            }
        }
    }
}
