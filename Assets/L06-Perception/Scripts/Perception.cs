using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L06
{
    public class Perception : MonoBehaviour
    {
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
                e.Invoke(c);
            }
        }

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
