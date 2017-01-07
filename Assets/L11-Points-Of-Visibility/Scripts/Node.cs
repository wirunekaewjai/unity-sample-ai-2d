using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L11
{
    public class Node : MonoBehaviour
    {
        [SerializeField]
        private List<Node> m_Neighbors = new List<Node>();

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
            Gizmos.color = Color.yellow;

            Vector2 p0 = transform.position;
            foreach (var neighbor in m_Neighbors)
            {
                Vector2 p1 = neighbor.transform.position;
                Gizmos.DrawLine(p0, p1);
            }
        }
    }
}
