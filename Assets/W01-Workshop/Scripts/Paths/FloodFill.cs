using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W01
{
    public class FloodFill : MonoBehaviour 
    {
        public float size = 1f;
        public int division = 5;
        public bool eightDirection = false;

        public List<string> obstacleTags = new List<string>();

        public bool gizmos = true;

        public Color walkableGizmosColor = Color.grey;
        public Color obstacleGizmosColor = Color.black;

        [SerializeField, HideInInspector]
        private List<Node> m_Nodes = new List<Node>();

        public Node FindNearest(Vector2 position)
        {
//            Vector2 center = transform.position;
//            float extents = size * division * 0.5f;
//
//            if (position.x >= center.x - extents && position.x <= center.x + extents &&
//               position.y >= center.y - extents && position.y <= center.y + extents)
//            {
                return (from n in m_Nodes
                    orderby (n.position - position).sqrMagnitude ascending
                    select n).First();
//            }
//
//            return null;
        }

        public void Generate()
        {
            m_Nodes.Clear();

            Vector2 center = transform.position;
            Vector2 extents = Vector2.one * size * division * 0.5f;
            Vector2 min = center - extents;

            Node[,] map = new Node[division, division];

            for (int y = 0; y < division; y++)
            {
                for (int x = 0; x < division; x++)
                {
                    Vector2 p0 = min + (new Vector2(x, y) * size);
                    Vector2 p1 = min + (new Vector2(x + 1, y + 1) * size);

                    Node node = new Node();
                    node.position = (p0 + p1) / 2f;
                    node.size = new Vector2(size, size);

                    map[x, y] = node;
                    m_Nodes.Add(node);

                    if (x > 0 && null != map[x - 1, y])
                    {
                        map[x - 1, y].neighbors.Add(node);
                        node.neighbors.Add(map[x - 1, y]);
                    }

                    if (y > 0 && null != map[x, y - 1])
                    {
                        map[x, y - 1].neighbors.Add(node);
                        node.neighbors.Add(map[x, y - 1]);
                    }


                    if (eightDirection)
                    {
                        // Left-Under
                        if (x > 0 && y > 0 && null != map[x - 1, y - 1])
                        {
                            map[x - 1, y - 1].neighbors.Add(node);
                            node.neighbors.Add(map[x - 1, y - 1]);
                        }

                        // Right-Under
                        if (x < division - 1 && y > 0 && null != map[x + 1, y - 1])
                        {
                            map[x + 1, y - 1].neighbors.Add(node);
                            node.neighbors.Add(map[x + 1, y - 1]);
                        }
                    }

                    Collider2D[] colliders = Physics2D.OverlapAreaAll(p0, p1);

                    if (colliders.Length == 0)
                    {
                        node.walkable = true;
                    }
                    else if (null != obstacleTags && obstacleTags.Count > 0)
                    {
                        bool walkable = true;

                        foreach (var collider in colliders)
                        {
                            if (obstacleTags.Contains(collider.tag))
                            {
                                walkable = false;
                                break;
                            }
                        }

                        node.walkable = walkable;
                    }

//                    if (null != collider)
//                    {
//                        if (null != obstacleTags)
//                        {
//                            if (obstacleTags.Count > 0 && !obstacleTags.Contains(collider.tag))
//                            {
//                                node.walkable = true;
//                                continue;
//                            }
//                        }
//
//                        node.walkable = false;
//                    }
//                    else
//                    {
//                        
//                    }
                }
            }

        }

        void OnValidate()
        {
            division = Mathf.Max(division, 2);
            Generate();
        }

        void OnDrawGizmos()
        {
            if (!gizmos)
                return;

            if (null != m_Nodes)
            {
                Vector3 offset = Vector3.forward;

                foreach (var node in m_Nodes)
                {
                    if (node.walkable)
                    {
                        Vector3 position = node.position;

                        Gizmos.color = walkableGizmosColor;
                        Gizmos.DrawWireCube(position + offset, node.size);
                    }
                    else
                    {
                        Vector3 position = node.position;

                        Gizmos.color = obstacleGizmosColor;
                        Gizmos.DrawWireCube(position + offset + offset, node.size);
                    }
                }
            }


        }
    }
}
