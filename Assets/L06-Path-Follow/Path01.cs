using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Path01 : MonoBehaviour 
    {
        [SerializeField]
        private List<Vector2> m_Points = new List<Vector2>();

        public Vector2 GetPoint(int index)
        {
            return m_Points[index];
        }

        public void SetPoint(int index, Vector2 point)
        {
            m_Points[index] = point;
        }

        public int Count
        {
            get { return m_Points.Count; }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            int lastIndex = Count - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                Vector2 p1 = GetPoint(i);
                Vector2 p2 = GetPoint(i + 1);

                Gizmos.DrawLine(p1, p2);
            }
        }
    }
}
