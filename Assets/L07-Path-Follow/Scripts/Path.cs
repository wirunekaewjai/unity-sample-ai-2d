using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L07
{
    public class Path : MonoBehaviour 
    {
        public bool isLoop;

        [SerializeField]
        private Point[] m_Points = { };

        public Point GetPoint(int index)
        {
            if (isLoop)
            {
                if(index >= m_Points.Length)
                {
                    index -= m_Points.Length;
                }
            }

            return m_Points[index];
        }

        public int Count
        {
            get 
            { 

                if (isLoop)
                {
                    return m_Points.Length + 1;
                }

                return m_Points.Length; 
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            int lastIndex = Count - 1;
            for (int i = 0; i < lastIndex; i++)
            {
                Vector2 p1 = GetPoint(i).Position;
                Vector2 p2 = GetPoint(i + 1).Position;

                Gizmos.DrawLine(p1, p2);
            }
        }
    }
}
