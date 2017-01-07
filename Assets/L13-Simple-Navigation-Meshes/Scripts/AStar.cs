using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.L13
{
    public static class AStar 
    {
        class Cost
        {
            public Node parent;

            public float h;
            public float f;
            public float g;
        }

        public static List<Node> Search(Node start, Node goal)
        {
            List<Node> path = new List<Node>();
            List<Node> opens = new List<Node>();

            Dictionary<Node, Cost> costs = new Dictionary<Node, Cost>();

            costs.Add(start, new Cost());
            opens.Add(start);

            int limit = 1000;
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
                            // && !closes.Contains(neighbor))
                        {
                            Cost cost = new Cost();

                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Distance(lowestF, neighbor);
                            cost.h = Distance(neighbor, goal);
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

        private static float Distance(Node a, Node b)
        {
            return Vector2.Distance(a.Mesh.Center, b.Mesh.Center);
        }
    }
}
