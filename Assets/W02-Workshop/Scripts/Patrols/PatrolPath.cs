using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class PatrolPath : MonoBehaviour 
    {
        public bool drawGizmos = true;

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
            if (!drawGizmos)
                return;

            foreach (var point in m_Points)
            {
                point.DrawGizmos();
            }
        }
    }
}
