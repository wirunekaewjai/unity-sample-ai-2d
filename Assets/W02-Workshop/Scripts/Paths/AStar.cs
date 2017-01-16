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

            public float h;
            public float f;
            public float g;
        }

        public static List<Node> Search(List<Node> nodes, Node start, Node goal, Heuristic heuristic)
        {
            List<Node> path = new List<Node>();
            List<Node> opens = new List<Node>();

            Dictionary<Node, Cost> costs = new Dictionary<Node, Cost>();

            costs.Add(start, new Cost());
            opens.Add(start);

            int limit = 65535;
            while (opens.Count > 0 && limit > 0)
            {
                limit--;

                Node lowestF = GetLowestF(opens, costs);

                opens.Remove(lowestF);

                if (lowestF != goal)
                {
                    int count = lowestF.GetNeighborCount();
                    for (int i = 0; i < count; i++)
                    {
                        int nodeIndex = lowestF.GetNeighbor(i);
                        Node neighbor = nodes[nodeIndex];

                        if (!costs.ContainsKey(neighbor))
                        {
                            Cost cost = new Cost();

                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Distance(lowestF, neighbor, heuristic);
                            cost.h = Distance(neighbor, goal, heuristic);
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

        private static Node GetLowestF(List<Node> opens, Dictionary<Node, Cost> costs)
        {
            IOrderedEnumerable<Node> nodes = (from n in opens
                orderby costs[n].f ascending
                select n);

            return nodes.First();
        }

        private static float Distance(Node a, Node b, Heuristic heuristic)
        {
            Vector2 p1 = b.ClosestPoint(a.Position);
            Vector2 p2 = a.ClosestPoint(b.Position);

            if (heuristic == Heuristic.Manhattan)
            {
                return Mathf.Abs(p1.x - p2.x) + Mathf.Abs(p1.y - p2.y);
            }

            // Euclidean
            return Vector2.Distance(p1, p2);
        }
    }
}
