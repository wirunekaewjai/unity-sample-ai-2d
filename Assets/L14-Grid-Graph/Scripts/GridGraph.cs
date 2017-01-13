using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.L14
{
    public class GridGraph : MonoBehaviour 
    {
        [Header("Debug")]
        public bool drawGizmos = true;

        [Space]
        public bool drawMesh = false;
        public Color meshColor = Color.cyan;

        [Space]
        public bool drawBorder = true;
        public Color borderColor = new Color(0,0,0,0.1f);

        [Header("Settings"), Range(1, 100)]
        public int overallSize = 10;
        public float minSize = 0.5f;

        [SerializeField]
        private List<Node> m_Nodes = new List<Node>();

//        [SerializeField]
//        private List<Node>[] m_Connections = {};
//        private Dictionary<Node, List<Node>> m_Graph = new Dictionary<Node, List<Node>>();

        public static List<Node> Search(Vector2 start, Vector2 goal, Heuristic heuristic)
        {
            var graph = FindObjectOfType<GridGraph>();

            var a = graph.FindNearest(start);
            var b = graph.FindNearest(goal);

            List<Node> nodes = AStar.Search(graph.m_Nodes, a, b, heuristic);

            return nodes;
        }

        public Node FindNearest(Vector2 position)
        {
            var graph = FindObjectOfType<GridGraph>();

            foreach (var node in m_Nodes)
            {
                Bounds b = new Bounds(node.position, node.size);

                if (b.Contains(position))
                    return node;
            }

            return (from n in m_Nodes
                orderby (n.position - position).sqrMagnitude ascending
                select n).First();
        }


        [ContextMenu("Generate")]
        public void Generate()
        {
            m_Nodes = new List<Node>();
//            m_Graph = new Dictionary<Node, List<Node>>();

//            Dictionary<Node, List<Node>> neighbors = new Dictionary<Node, List<Node>>();
            CreateNodes(m_Nodes, transform.position, overallSize, 10); 
//            ConnectNodes(m_Nodes);

//            foreach (var item in neighbors)
//            {
//                item.Key.neighbors.AddRange(item.Value);
//                m_Nodes.Add(item.Key);
//            }
        }

//        private bool IsCollinear(Edge a, Edge b)
//        {
//            Vector2 e0 = (a.p1 - a.p0);
//            Vector2 e1 = (b.p0 - a.p0);
//            Vector2 e2 = (b.p1 - a.p0);
//
//            Vector2 e3 = (b.p0 - a.p1);
//            Vector2 e4 = (b.p1 - a.p1);
//
//            float c0 = Mathf.Abs(Vector3.Cross(e0, e1).z);
//            float c1 = Mathf.Abs(Vector3.Cross(e0, e2).z);
//
//            if (c0 <= Mathf.Epsilon)
//            {
//                float d0 = e0.sqrMagnitude;
//                float d1 = e1.sqrMagnitude;
//                float d2 = e3.sqrMagnitude;
//
//                if (Mathf.Abs(d0 - (d1 + d2)) <= Mathf.Epsilon)
//                {
//                    return true;
//                }
//            }
//            else if(c1 <= Mathf.Epsilon)
//            {
//                float d0 = e0.sqrMagnitude;
//                float d1 = e2.sqrMagnitude;
//                float d2 = e4.sqrMagnitude;
//
//                if (Mathf.Abs(d0 - (d1 + d2)) <= Mathf.Epsilon)
//                {
//                    return true;
//                }
//            }
//
//            return false;
//        }

        private Edge[] GetEdges(Node node)
        {
            Vector2 extents = node.size / 2f;
            Vector2 min = node.position - extents;
            Vector2 max = node.position + extents;

            Vector2 lb = min;
            Vector2 lt = new Vector2(min.x, max.y);
            Vector2 rt = max;
            Vector2 rb = new Vector2(max.x, min.y);

            Edge[] edges = new Edge[4];

            edges[0] = new Edge(lb, lt);
            edges[1] = new Edge(lt, rt);
            edges[2] = new Edge(rt, rb);
            edges[3] = new Edge(rb, lb);

            return edges;
        }

        private bool IsConnectable(Node a, Node b, float radius)
        {

            // Step 1
            float scale = 1f - Mathf.Epsilon;

            Bounds ba = new Bounds(a.position, a.size * scale);
            Bounds bb = new Bounds(b.position, b.size * scale);

            if (!ba.Intersects(bb))
                return false;

            // Step 2
            Edge[] eas = GetEdges(a);
            Edge[] ebs = GetEdges(b);

            float sqr0 = a.size.sqrMagnitude;

            foreach (var eb in ebs)
            {
                Bounds bb1 = new Bounds(a.position, a.size);

                bb1.Encapsulate(eb.p0);
                bb1.Encapsulate(eb.p1);

                float sqr1 = bb1.size.sqrMagnitude;

                if (Mathf.Abs(sqr1 - sqr0) <= Mathf.Epsilon)
                {
                    return true;
                }
            }

            sqr0 = b.size.sqrMagnitude;

            foreach (var ea in eas)
            {
                Bounds ea1 = new Bounds(b.position, b.size);

                ea1.Encapsulate(ea.p0);
                ea1.Encapsulate(ea.p1);

                float sqr1 = ea1.size.sqrMagnitude;

                if (Mathf.Abs(sqr1 - sqr0) <= Mathf.Epsilon)
                {
                    return true;
                }
            }

            // Step 3
            Vector2 p0 = a.position;
            Vector2 dir = (b.position - a.position).normalized;

            RaycastHit2D hit = Physics2D.CircleCast(p0, radius, dir);

            if (null == hit.collider)
            {
                return true;
            }

            return false;
        }

//        private void ConnectNodes(List<Node> nodes)
//        {
//            for (int i = 0; i < nodes.Count - 1; i++)
//            {
//                Node a = nodes[i];
//
//                for (int j = i + 1; j < nodes.Count; j++) 
//                {
//                    Node b = nodes[j];
//
//                    if (IsConnectable(a, b))
//                    {
////                        graph[a].Add(b);
////                        graph[b].Add(a);
//                        a.neighbors.Add(b);
//                        b.neighbors.Add(a);
//                    }
//                }
//            }
//        }

        private void CreateNodes(List<Node> nodes, Vector2 center, float size, int ttl)
        {
            if (ttl <= 0 || size < minSize)
                return;
            
            Vector2 min = center - (Vector2.one * size * 0.5f);
            float cellSize = size / 2f;

            Vector2 offset = new Vector2(minSize, minSize);

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Vector2 p0 = min + (new Vector2(x, y) * cellSize) - offset;
                    Vector2 p1 = min + (new Vector2(x + 1, y + 1) * cellSize) + offset;

                    if (null == Physics2D.OverlapArea(p0, p1))
                    {
                        Node node = new Node();

                        node.position = (p0 + p1) / 2f;
                        node.size = Vector2.one * cellSize;

                        for (int i = 0; i < nodes.Count; i++)
                        {
                            Node neighbor = nodes[i];

                            if (IsConnectable(node, neighbor, minSize))
                            {
                                node.neighbors.Add(i);
                                neighbor.neighbors.Add(nodes.Count);
                            }
                        }

                        nodes.Add(node);
                    }
                    else
                    {
                        CreateNodes(nodes, (p0 + p1) / 2f, cellSize, ttl - 1);
                    }
                }
            }

        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;
            
            Gizmos.color = new Color(0, 0, 0, 0.25f);
            Gizmos.DrawWireCube(transform.position, Vector2.one * overallSize);

            foreach (var node in m_Nodes)
            {
                if (drawMesh)
                {
                    Gizmos.color = meshColor;
                    Gizmos.DrawCube(node.position, node.size);
                }

                if (drawBorder)
                {
                    Gizmos.color = borderColor;
                    Gizmos.DrawWireCube(node.position, node.size);
                }

//                Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.1f);
//                foreach (var index in node.neighbors)
//                {
//                    Gizmos.DrawLine(node.position, m_Nodes[index].position);
//                }
            }

        }
    }
}
