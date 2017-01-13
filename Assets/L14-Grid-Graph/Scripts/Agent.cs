using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L14
{
    public class Agent : MonoBehaviour 
    {
        public Transform target;
        public Heuristic heuristic = Heuristic.Euclidean;
        public float radius = 0.5f;
        public bool clean = false;

        private List<Node> m_Nodes = new List<Node>();
        private List<Vector2> m_Path = new List<Vector2>();

        private void Update()
        {
            if (null == target)
                return;

            if (Input.GetKeyDown(KeyCode.A))
            {
                m_Nodes = GridGraph.Search(transform.position, target.position, heuristic);

//                for (int i = 0; i < m_Nodes.Count - 1; i++)
//                {
//                    var n0 = m_Nodes[i];
//                    var n1 = m_Nodes[i + 1];
//
//                    Debug.DrawLine(n0.position, n1.position, Color.red, 1f);
//                }

                m_Path = new List<Vector2>();

                Vector2 start = transform.position;
                Vector2 goal = target.position;

                for (int i = m_Nodes.Count - 1; i > 0; i--)
                {
                    Node n0 = m_Nodes[i];
                    Node n1 = m_Nodes[i - 1];

                    Bounds b0 = new Bounds(n0.position, n0.size);
                    Bounds b1 = new Bounds(n1.position, n1.size);

                    Vector2 p0 = b0.ClosestPoint(start);
                    Vector2 p1 = b0.ClosestPoint(goal);
                    Vector2 p2 = (p0 + p1) / 2f;

                    Vector2 p3 = b1.ClosestPoint(p2);

                    m_Path.Insert(0, p3);
                }

                m_Path.Insert(0, start);
                m_Path.Add(goal);

                if (clean)
                {
                    for (int i = 0; i < m_Path.Count - 2;)
                    {
                        Vector2 p0 = m_Path[i];
                        Vector2 p1 = m_Path[i + 1];
                        Vector2 p2 = m_Path[i + 2];

                        RaycastHit2D h0 = Physics2D.CircleCast(p0, radius, p2 - p0);

                        if (null == h0.collider)
                        {
                            m_Path.RemoveAt(i + 1);
                        }
                        else
                        {
                            Vector2 p3 = (p0 + p1 + p2) / 3f;

                            RaycastHit2D h1 = Physics2D.CircleCast(p0, radius, p3 - p0);
                            RaycastHit2D h2 = Physics2D.CircleCast(p2, radius, p2 - p3);

                            if (null == h1.collider && null == h2.collider)
                            {
                                m_Path[i + 1] = p3;
                            }
                            else
                            {
                                m_Path[i + 1] = p1;
                            }

                            i++;
                        }
                    }
                }

            }
        }

        private void OnDrawGizmos()
        {
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireSphere(transform.position, radius);

            Gizmos.color = Color.yellow;
            for (int i = 0; i < m_Path.Count; i++)
            {
                Gizmos.DrawWireCube(m_Path[i], Vector2.one * 0.1f);
            }

            Gizmos.color = new Color(1, 1, 0, 0.5f);
            for (int i = 0; i < m_Path.Count - 1; i++)
            {
                Gizmos.DrawLine(m_Path[i], m_Path[i + 1]);
            }

        }
    }

}