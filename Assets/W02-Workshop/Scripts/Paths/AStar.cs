using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W02
{
    // Copied + Edited from 'L11-Points-Of-Visibility'
    public static class AStar 
    {
        class Cost
        {
            public Node parent;
            public Vector2 closest;

            public float h;
            public float f;
            public float g;
        }

        public static List<Node> Search(List<Node> nodes, Vector2 start, Vector2 goal, Heuristic heuristic)
        {
            List<Node> path = new List<Node>();
            List<Node> opens = new List<Node>();

            Node a = FindNearest(nodes, start);
            Node b = FindNearest(nodes, goal);

            goal = b.ClosestPoint(goal);

            Dictionary<Node, Cost> costs = new Dictionary<Node, Cost>();

            costs.Add(a, new Cost() { closest = a.ClosestPoint(start) });
            opens.Add(a);

            int limit = 65535;
            while (opens.Count > 0 && limit > 0)
            {
                limit--;

                Node lowestF = GetLowestF(opens, costs);

                opens.Remove(lowestF);

                if (lowestF != b)
                {
                    Vector2 p0 = costs[lowestF].closest;

                    int count = lowestF.GetNeighborCount();
                    for (int i = 0; i < count; i++)
                    {
                        int nodeIndex = lowestF.GetNeighbor(i);
                        Node neighbor = nodes[nodeIndex];

                        if (!costs.ContainsKey(neighbor))
                        {
                            Vector2 p1 = neighbor.ClosestPoint(p0);
                            Vector2 p2 = neighbor.ClosestPoint(goal);

                            Cost cost = new Cost();

                            cost.closest = p1;
                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Distance(p0, p1, heuristic);
                            cost.h = Distance(p2, goal, heuristic);
                            cost.f = cost.g + cost.h;

                            costs.Add(neighbor, cost);
                            opens.Add(neighbor);
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

            return path;
        }

        private static Node FindNearest(List<Node> nodes, Vector2 position)
        {
            foreach (var node in nodes)
            {
                if (node.Contains(position))
                    return node;
            }

            return (from n in nodes
                orderby (n.Position - position).sqrMagnitude ascending
                select n).First();
        }

        private static Node GetLowestF(List<Node> opens, Dictionary<Node, Cost> costs)
        {
            IOrderedEnumerable<Node> nodes = (from n in opens
                orderby costs[n].f ascending
                select n);

            return nodes.First();
        }

        private static float Distance(Vector2 a, Vector2 b, Heuristic heuristic)
        {
            if (heuristic == Heuristic.Manhattan)
            {
                return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
            }

            // Euclidean
            return Vector2.Distance(a, b);
        }
    }
}
