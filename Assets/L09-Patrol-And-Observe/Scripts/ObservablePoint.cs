using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L09
{
    using Point = Wirune.L07.Point;

    public class ObservablePoint : Point 
    {
        [SerializeField]
        private float m_Duration = 1;

        [SerializeField]
        private Vector2[] m_Direction = {};

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
