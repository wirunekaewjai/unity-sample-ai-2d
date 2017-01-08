using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class ObservablePoint : PatrolPoint 
    {
        [SerializeField]
        private float m_Duration = 1;

        [SerializeField]
        private Vector2[] m_Direction = {};

        public override void DrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0);
            Gizmos.DrawSphere(transform.position, 0.2f);
        }

        public Vector2 GetDirection(int index)
        {
            return m_Direction[index];
        }

        public int Count
        {
            get 
            { 
                return m_Direction.Length; 
            }
        }

        public float Duration
        {
            get
            {
                return m_Duration;
            }
        }
    }
}
