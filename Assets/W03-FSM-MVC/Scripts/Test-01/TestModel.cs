using UnityEngine;

namespace Wirune.W03.Test01
{
    public class TestModel : Model
    {
        [SerializeField]
        private int m_Point;

        public int Point
        {
            get { return m_Point; }
            set
            {
                m_Point = value;
                Notify("PointChanged", m_Point);
            }
        }

        void OnEnable()
        {
            Notify("PointChanged", Point);
        }
    }
}
