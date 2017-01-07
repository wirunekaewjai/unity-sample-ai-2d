using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L13
{
    public class Edge : MonoBehaviour
    {
        [SerializeField]
        private Vertex m_V0;

        [SerializeField]
        private Vertex m_V1;

        public Vector2 P0
        {
            get
            {
                return m_V0.Position;
            }
        }

        public Vector2 P1
        {
            get
            {
                return m_V1.Position;
            }
        }

        public Vector2 Center
        {
            get
            {
                return (P0 + P1) / 2f;
            }
        }

        public void DrawGizmos(Color color)
        {
            if (null == m_V0 || null == m_V1)
                return;

            Gizmos.color = color;

            Vector2 p0 = m_V0.Position;
            Vector2 p1 = m_V1.Position;

            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawWireSphere((p0 + p1) / 2f, 0.05f);
        }

        void OnDrawGizmosSelected()
        {
            DrawGizmos(Color.yellow);
        }
    }
}
