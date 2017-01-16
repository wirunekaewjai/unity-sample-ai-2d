using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class PatrolPoint : MonoBehaviour 
    {
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