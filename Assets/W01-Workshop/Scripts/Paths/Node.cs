using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W01
{
    // Copied from 'L12-Flood-Fill'
    public class Node
    {
        public bool walkable;

        public Vector2 position;
        public Vector2 size;

        public readonly List<Node> neighbors = new List<Node>();



//        public Node FindWalkable(Vector2 position, Vector2 direction)
//        {
//            Vector2 n0 = (position - this.position).normalized;
//
//            Node nearest = null;
//            float maxDot = 0f;
//
//            foreach (var neighbor in neighbors)
//            {
//                if (!neighbor.walkable)
//                    continue;
//
//                Vector2 n1 = (neighbor.position - this.position).normalized;
//
//                if (Vector2.Dot(n0, n1) < 0)
//                    continue;
//
//                Vector2 n2 = (neighbor.position - position).normalized;
//                float dot = Vector2.Dot(n2, direction);
//
//                if (dot >= maxDot)
//                {
//                    maxDot = dot;
//                    nearest = neighbor;
//                }
//            }
//
//            return nearest;
//        }

        public Node this[int index]
        {
            get 
            {
                return neighbors[index];
            }
        }

        public int Count
        {
            get
            {
                return neighbors.Count;
            }
        }
    }
}

