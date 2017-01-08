using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class Perception : MonoBehaviour
    {
//        [SerializeField]
//        private Vector2 m_Size = new Vector2(1, 1);

        [SerializeField]
        private string[] m_Tags = {};

        [SerializeField]
        private Collider2DEvent m_OnTriggerEnter2DEvent;

        [SerializeField]
        private Collider2DEvent m_OnTriggerStay2DEvent;

        [SerializeField]
        private Collider2DEvent m_OnTriggerExit2DEvent;

        void RaiseEvent(Collider2DEvent e, Collider2D c)
        {
            if (m_Tags.Length == 0 || System.Array.IndexOf(m_Tags, c.tag) >= 0)
            {
                e.Invoke(this, c);
            }
        }

//        private Collider2D m_Temp;
//
//        void Update()
//        {
//            Vector2 origin = transform.position;
//            Vector2 size = m_Size;
//
//            float angle = transform.eulerAngles.z;
//
//            Collider2D c = Physics2D.OverlapBox(origin, size, angle);
//            if (null != c)
//            {
//                if (m_Temp != c)
//                {
//                    RaiseEvent(m_OnTriggerEnter2DEvent, c);
//                    m_Temp = c;
//                }
//            }
//        }
//
//        void OnDrawGizmos()
//        {
//            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
//            Gizmos.color = Color.green;
//
//            Gizmos.DrawWireCube(Vector2.zero, m_Size);
//        }

        void OnTriggerEnter2D(Collider2D c)
        {
            RaiseEvent(m_OnTriggerEnter2DEvent, c);
        }

        void OnTriggerStay2D(Collider2D c)
        {
            RaiseEvent(m_OnTriggerStay2DEvent, c);
        }

        void OnTriggerExit2D(Collider2D c)
        {
            RaiseEvent(m_OnTriggerExit2DEvent, c);
        }
    }


}
