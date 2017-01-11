using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class PatrolPoint : MonoBehaviour 
    {
//        private Node m_Node;
//
//        public Node Node
//        {
//            get
//            {
//                if (null == m_Node)
//                {
//                    m_Node = FindObjectOfType<GridGraph>().FindNearest(transform.position);
//                }
//
//                return m_Node;
//            }
//        }

        public virtual void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
        }
    }
}