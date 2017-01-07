using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public class TestPathFinding01 : MonoBehaviour
    {
        // SerializeField
        public Node start;
        public Node goal;

        // Non-Serialized
        private List<Node> m_Path = new List<Node>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (null != start && null != goal)
                {
                    m_Path = AStar.Search(start, goal);
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var node in m_Path)
            {
                Vector2 p = node.Mesh.Center;
                Gizmos.DrawWireSphere(p, 0.15f);
            }
        }
    }
}

