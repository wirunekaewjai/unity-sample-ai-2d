using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class PatrolPath : MonoBehaviour 
    {
        public bool gizmos = true;

        [SerializeField]
        private PatrolPoint[] m_Points = { };

        public PatrolPoint GetPoint(int index)
        {
            return m_Points[index];
        }

        public int Count
        {
            get 
            { 
                return m_Points.Length; 
            }
        }

        void OnDrawGizmos()
        {
            if (!gizmos)
                return;

            Gizmos.color = Color.green;

            int lastIndex = Count - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                Vector2 p1 = GetPoint(i).Position;
                Vector2 p2 = GetPoint(i + 1).Position;

                Gizmos.DrawLine(p1, p2);
            }

            foreach (var point in m_Points)
            {
                point.DrawGizmos();
            }
        }
    }
}
