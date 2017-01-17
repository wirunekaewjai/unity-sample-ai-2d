using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L14
{
    public class Graph : MonoBehaviour 
    {
        [Header("Debug")]
        public bool drawGizmos = true;

        [Space]
        public bool drawMesh = false;
        public Color meshColor = Color.cyan;

        [Space]
        public bool drawBorder = true;
        public Color borderColor = new Color(0,0,0,0.1f);

        [Space]
        public bool drawConnection = false;
        public Color connectionColor = new Color(1,0.5f,0,0.25f);

        [Header("Settings"), Range(1, 1000)]
        public int overallSize = 10;
        public float minSize = 0.5f;

        [Space]
        public ushort mergeIterationLimit = 10;

        [SerializeField]
        private List<Node> m_Nodes = new List<Node>();

        // Property
        public List<Node> Nodes { get { return m_Nodes; } }

        public static List<Vector3> Search(Vector3 start, Vector3 goal, Heuristic heuristic, float radius)
        {
            Graph graph = FindObjectOfType<Graph>();
            GameObject exclude = graph.gameObject;

            radius = Mathf.Max(radius, graph.minSize);

            return AStar.Search(graph.m_Nodes, start, goal, heuristic, radius, exclude);
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = new Color(0, 0, 0, 1f);
            Gizmos.DrawWireCube(transform.position, new Vector3(overallSize, 0, overallSize));

            Vector3 offset = new Vector3(0, transform.position.y + 0.01f, 0);

            if (drawMesh)
            {
                Gizmos.color = meshColor;
                foreach (var node in Nodes)
                {
                    Gizmos.DrawCube(node.Position + offset, node.Size);
                }
            }

            if (drawBorder)
            {
                Gizmos.color = borderColor;
                foreach (var node in Nodes)
                {
                    Gizmos.DrawWireCube(node.Position + offset, node.Size);
                }
            }

            if (drawConnection)
            {
                Gizmos.color = connectionColor;
                foreach (var a in m_Nodes)
                {
                    int neighborCount = a.GetNeighborCount();
                    for (int i = 0; i < neighborCount; i++)
                    {
                        int nodeIndex = a.GetNeighbor(i);
                        Node b = m_Nodes[nodeIndex];

                        Vector3 c0 = a.ClosestPoint(b.Position);
                        Vector3 c1 = b.ClosestPoint(a.Position);
                        Vector3 c2 = (c0 + c1) / 2f;

                        Gizmos.DrawLine(a.Position + offset, c2 + offset);
                    }
                }
            }
        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            m_Nodes = new List<Node>();

            CreateNodes(m_Nodes, transform.position, overallSize, 10); 
//            MergeNodes(m_Nodes);
            ConnectNodes(m_Nodes);
        }

        private void CreateNodes(List<Node> nodes, Vector3 center, float size, int ttl)
        {
            if (ttl <= 0 || size < minSize)
                return;

            float cellSize = size / 2f;

            Vector3 extents = new Vector3(cellSize, 0, cellSize);
            Vector3 min = center - extents;
//            Vector3 offset = new Vector3(minSize, minSize, minSize);

            Vector3 extents3D = new Vector3(cellSize, size, cellSize);

            for (int z = 0; z < 2; z++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Vector3 p0 = min + (new Vector3(x, 0, z) * cellSize);
                    Vector3 p1 = min + (new Vector3(x + 1, 0, z + 1) * cellSize);
                    Vector3 p2 = (p0 + p1) / 2f;

                    Collider[] colliders = Physics.OverlapBox(p2, extents3D);
                    bool overlap = false;

                    foreach (var collider in colliders)
                    {
                        GameObject obj = collider.gameObject;
                        if (obj != gameObject && obj.isStatic)
                        {
                            overlap = true;
                            break;
                        }
                    }

                    if (!overlap)
                    {
                        Node node = new Node();

                        node.Position = p2;
                        node.Size = extents;

                        for (int j = 0; j < nodes.Count; j++) 
                        {
                            Node b = nodes[j];

                            if (IsMergable(node, b))
                            {
                                node.Encapsulate(b);
                                nodes.RemoveAt(j--);
                            }
                        }

                        nodes.Add(node);
                    }
                    else
                    {
                        CreateNodes(nodes, p2, cellSize, ttl - 1);
                    }
                }
            }
        }

//        private void MergeNodes(List<Node> nodes)
//        {
//            int currentCount = nodes.Count;
//
//            for (int pass = 0; pass < mergeIterationLimit; pass++)
//            {
//                for (int i = 0; i < nodes.Count - 1; i++)
//                {
//                    Node a = nodes[i];
//
//                    for (int j = i + 1; j < nodes.Count; j++) 
//                    {
//                        Node b = nodes[j];
//
//                        if (IsMergable(a, b))
//                        {
//                            a.Encapsulate(b);
//                            nodes.RemoveAt(j--);
//                        }
//                    }
//                }
//
//                if (currentCount == nodes.Count)
//                    break;
//
//                currentCount = nodes.Count;
//            }
//        }

        private bool IsMergable(Node a, Node b)
        {
            // Step 1
            if (!a.Intersects(b))
                return false;

            // Step 2
            Vector3 dir = (b.Position - a.Position).normalized;

            float dx = Mathf.Abs(dir.x);
            float dz = Mathf.Abs(dir.z);

            float nearestZero = Mathf.Epsilon;
            float nearestOne = 0.99999f;

            if (dx <= nearestZero && dz >= nearestOne)
            {
                // Step 3
                return Mathf.Abs(a.Size.x - b.Size.x) <= nearestZero;
            }
            else if (dz <= nearestZero && dx >= nearestOne)
            {
                // Step 3
                return Mathf.Abs(a.Size.z - b.Size.z) <= nearestZero;
            }

            return false;
        }

        private void ConnectNodes(List<Node> nodes)
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Node a = nodes[i];

                for (int j = i + 1; j < nodes.Count; j++) 
                {
                    Node b = nodes[j];

                    if (a.Intersects(b))
                    {
                        a.AddNeighbor(j);
                        b.AddNeighbor(i);
                    }
                }
            }
        }

    }

}