using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L14
{
    [System.Serializable]
    public class Node
    {
//        public bool walkable;

        public Vector2 position;
        public Vector2 size;

        public List<int> neighbors = new List<int>();
//        public List<Node> neighbors = new List<Node>();

        public int this[int index]
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

