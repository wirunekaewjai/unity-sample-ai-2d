using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public class Vertex : MonoBehaviour 
    {
        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Position, 0.1f);
        }
    }
}
