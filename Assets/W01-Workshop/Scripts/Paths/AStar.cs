using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W01
{
    // Copied from 'L12-Flood-Fill'
    public static class AStar 
    {
        class Cost
        {
            public Node parent;

            public float h;
            public float f;
            public float g;
        }

        public static List<Node> Search(Node start, Node goal, Heuristic heuristic)
        {
            List<Node> path = new List<Node>();
            List<Node> opens = new List<Node>();

            Dictionary<Node, Cost> costs = new Dictionary<Node, Cost>();

            costs.Add(start, new Cost());
            opens.Add(start);

//            Debug.Log("Start: " + start.walkable);
            int limit = 65535;
            while (opens.Count > 0 && limit > 0)
            {
                limit--;

                Node lowestF = GetLowestF(opens, costs);

                opens.Remove(lowestF);

                if (lowestF != goal)
                {
                    int count = lowestF.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Node neighbor = lowestF[i];

                        if (!costs.ContainsKey(neighbor))
                        {
                            Cost cost = new Cost();

                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Distance(lowestF, neighbor, heuristic);
                            cost.h = Distance(neighbor, goal, heuristic);
                            cost.f = cost.g + cost.h;

                            costs.Add(neighbor, cost);

                            if (neighbor.walkable)
                            {
                                opens.Add(neighbor);
                            }
                        }
                    }
                }
                else
                {
                    Node node = lowestF;
                    Cost cost = costs[lowestF];

                    do
                    {
                        path.Insert(0, node);

                        cost = costs[node];
                        node = cost.parent;
                    }
                    while(null != node);

                    // End While-Loop
                    break;
                }
            }

            Clean(path);
            return path;
        }

        private static Node GetLowestF(List<Node> opens, Dictionary<Node, Cost> costs)
        {
            IOrderedEnumerable<Node> nodes = (from n in opens
                where n.walkable
                orderby costs[n].f ascending
                select n);

            return nodes.FirstOrDefault();
        }

        private static float Distance(Node a, Node b, Heuristic heuristic)
        {
            Vector2 p1 = a.position;
            Vector2 p2 = b.position;

            if (heuristic == Heuristic.Manhattan)
            {
                return Mathf.Abs(p1.x - p2.x) + Mathf.Abs(p1.y - p2.y);
            }

            // Euclidean
            return Vector2.Distance(p1, p2);
        }

        private static void Clean(List<Node> path)
        {
            for (int i = 0; i < path.Count - 2;)
            {
                Node n0 = path[i];
                Node n1 = path[i + 1];
                Node n2 = path[i + 2];

                Vector2 v0 = (n1.position - n0.position).normalized;
                Vector2 v1 = (n2.position - n1.position).normalized;

                if (Vector2.Dot(v0, v1) >= 0.8f)
                {
                    path.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
