using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public class TestPathFinding : MonoBehaviour
    {
        // SerializeField
        public FloodFill floodFill;

        public Heuristic heuristic = Heuristic.Euclidean;
        public bool smooth = true;

        public Transform start;
        public Transform goal;

        // Non-Serialized
        private List<Node> m_Path = new List<Node>();

        public void Search()
        {
            if (null != start && null != goal)
            {
                Node a = floodFill.FindNearest(start.position);
                Node b = floodFill.FindNearest(goal.position);

                m_Path = AStar.Search(a, b, heuristic);

                if (smooth)
                {
                    PathSmoother.Smooth(m_Path);
                }
            }
        }

        void OnValidate()
        {
            if (Application.isPlaying)
            {
                Search();
            }
        }

        void Start()
        {
            Search();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Search();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var node in m_Path)
            {
                Gizmos.DrawWireSphere(node.position, 0.05f);
            }

            for (int i = 0; i < m_Path.Count - 1; i++)
            {
                Vector2 p0 = m_Path[i].position;
                Vector2 p1 = m_Path[i + 1].position;

                Gizmos.DrawLine(p0, p1);
            }
        }
    }
}

