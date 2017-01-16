using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L14
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

        private List<Vector3> m_Path = new List<Vector3>();

        private void Update()
        {
            if (null == target)
                return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                Vector3 start = transform.position;
                Vector3 goal = target.position;

                m_Path = Graph.Search(start, goal, heuristic, radius);
            }
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Vector3 offset = new Vector3(0, transform.position.y, 0);

            for (int i = 0; i < m_Path.Count; i++)
            {
                if (drawPath)
                {
                    Gizmos.color = pathColor;
                    Gizmos.DrawWireCube(m_Path[i] + offset, Vector3.one * 0.1f);
                }

                if (drawPathNode)
                {
                    Gizmos.color = pathNodeColor;
                    Gizmos.DrawWireSphere(m_Path[i] + offset, radius);
                }
            }

            if (drawPath)
            {
                Gizmos.color = pathColor;

                if (m_Path.Count > 0)
                {
                    Gizmos.DrawLine(transform.position, m_Path[0] + offset);
                }

                for (int i = 0; i < m_Path.Count - 1; i++)
                {
                    Gizmos.DrawLine(m_Path[i] + offset, m_Path[i + 1] + offset);
                }
            }

        }
    }

}