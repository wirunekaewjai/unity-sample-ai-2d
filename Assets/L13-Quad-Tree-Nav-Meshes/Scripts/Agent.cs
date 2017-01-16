using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L13
{
    public class Agent : MonoBehaviour 
    {
        [Header("Gizmos")]
        public bool drawGizmos = true;

        [Space]
        public bool drawPath = true;
        public Color pathColor = Color.yellow;

        [Space]
        public bool drawPathNode = false;
        public Color pathNodeColor = new Color(1, 1, 0, 0.25f);

        [Header("Settings")]
        public Transform target;
        public Heuristic heuristic = Heuristic.Euclidean;
        public float radius = 0.5f;

        private List<Vector2> m_Path = new List<Vector2>();

        private void Update()
        {
            if (null == target)
                return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                Vector2 start = transform.position;
                Vector2 goal = target.position;

                m_Path = Graph.Search(start, goal, heuristic, radius);
            }
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;
            
            for (int i = 0; i < m_Path.Count; i++)
            {
                if (drawPath)
                {
                    Gizmos.color = pathColor;
                    Gizmos.DrawWireCube(m_Path[i], Vector2.one * 0.1f);
                }

                if (drawPathNode)
                {
                    Gizmos.color = pathNodeColor;
                    Gizmos.DrawWireSphere(m_Path[i], radius);
                }
            }

            if (drawPath)
            {
                Gizmos.color = pathColor;
                for (int i = 0; i < m_Path.Count - 1; i++)
                {
                    Gizmos.DrawLine(m_Path[i], m_Path[i + 1]);
                }
            }

        }
    }

}