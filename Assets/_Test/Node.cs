using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.POV
{
    public class Node : MonoBehaviour 
    {
        public List<Node> neighbors = new List<Node>();

        void OnValidate()
        {
            foreach (var n in neighbors)
            {
                if (!n.neighbors.Contains(this))
                {
                    n.neighbors.Add(this);   
                }
            }

            neighbors = neighbors.Distinct().ToList();  
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector2 p0 = transform.position;
            Gizmos.DrawWireSphere(p0, 0.1f);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;

            Vector2 p0 = transform.position;
            foreach (var neighbor in neighbors)
            {
                Vector2 p1 = neighbor.transform.position;
                Gizmos.DrawLine(p0, p1);
            }
        }
    }

}