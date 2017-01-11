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

