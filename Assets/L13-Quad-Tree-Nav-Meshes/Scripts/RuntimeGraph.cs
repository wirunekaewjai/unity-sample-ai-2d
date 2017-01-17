using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L13
{
    public class RuntimeGraph : MonoBehaviour 
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
        public Color connectionColor = new Color(1,0.5f,0,0.1f);

        [Space, Range(0.1f, 3f)]
        public float speed = 1f;

        [Header("Settings"), Range(1, 1000)]
        public int overallSize = 10;
        public float minSize = 0.5f;

        [Space]
        public ushort mergeIterationLimit = 10;

        [SerializeField]
        private List<Node> m_Nodes = new List<Node>();

        // Property
        public List<Node> Nodes { get { return m_Nodes; } }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

                m_Nodes = new List<Node>();
                StartCoroutine(CreateNodes(m_Nodes, transform.position, overallSize, 10));
                StartCoroutine(ConnectNodes(m_Nodes));
            }
        }

        private IEnumerator CreateNodes(List<Node> nodes, Vector2 center, float size, int ttl)
        {
            if (ttl > 0 && size >= minSize)
            {
                float cellSize = size / 2f;

                Vector2 min = center - (Vector2.one * size * 0.5f);
                Vector2 offset = new Vector2(minSize, minSize);

                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 2; x++)
                    {
                        yield return new WaitForSeconds(0.1f / speed);

                        Vector2 p0 = min + (new Vector2(x, y) * cellSize) - offset;
                        Vector2 p1 = min + (new Vector2(x + 1, y + 1) * cellSize) + offset;
                        Vector2 p2 = (p0 + p1) / 2f;

                        Collider2D collider = Physics2D.OverlapArea(p0, p1);

                        if (null == collider || !collider.gameObject.isStatic)
                        {
                            Node node = new Node();

                            node.Position = p2;
                            node.Size = Vector2.one * cellSize;

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
                            yield return new WaitForSeconds(0.1f / speed);
                            StartCoroutine(CreateNodes(nodes, p2, cellSize, ttl - 1));
                        }
                    }
                }


            }
        }

        private bool IsMergable(Node a, Node b)
        {
            // Step 1
            if (!a.Intersects(b))
                return false;

            // Step 2
            Vector2 dir = (b.Position - a.Position).normalized;

            float dx = Mathf.Abs(dir.x);
            float dy = Mathf.Abs(dir.y);

            float nearestZero = Mathf.Epsilon;
            float nearestOne = 0.99999f;

            if (dx <= nearestZero && dy >= nearestOne)
            {
                // Step 3
                return Mathf.Abs(a.Size.x - b.Size.x) <= nearestZero;
            }
            else if (dy <= nearestZero && dx >= nearestOne)
            {
                // Step 3
                return Mathf.Abs(a.Size.y - b.Size.y) <= nearestZero;
            }

            return false;
        }

        private IEnumerator ConnectNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                node.ClearNeighbors();
            }

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

                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = new Color(0, 0, 0, 0.25f);
            Gizmos.DrawWireCube(transform.position, Vector2.one * overallSize);

            if (drawMesh)
            {
                Gizmos.color = meshColor;
                foreach (var node in Nodes)
                {
                    Gizmos.DrawCube(node.Position, node.Size);
                }
            }

            if (drawBorder)
            {
                Gizmos.color = borderColor;
                foreach (var node in Nodes)
                {
                    Gizmos.DrawWireCube(node.Position, node.Size);
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

                        Vector2 c0 = a.ClosestPoint(b.Position);
                        Vector2 c1 = b.ClosestPoint(a.Position);
                        Vector2 c2 = (c0 + c1) / 2f;

                        Gizmos.DrawLine(a.Position, c2);
                    }
                }
            }
        }
    }
}
