using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Path02 : MonoBehaviour 
    {
        public bool isLoop;

        [SerializeField]
        private List<Vector2> m_Points = new List<Vector2>();

        public Vector2 GetPoint(int index)
        {
            if (isLoop && index >= m_Points.Count)
            {
                index -= m_Points.Count;
            }

            return m_Points[index];
        }

        public void SetPoint(int index, Vector2 point)
        {
            if (isLoop && index >= m_Points.Count)
            {
                index -= m_Points.Count;
            }

            m_Points[index] = point;
        }

        public int Count
        {
            get 
            { 
                if (isLoop)
                {
                    return m_Points.Count + 1;
                }

                return m_Points.Count; 
            }
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