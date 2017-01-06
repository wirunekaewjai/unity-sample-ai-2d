using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.POV
{
    public class AStar : MonoBehaviour 
    {
        public Node start;
        public Node goal;

        public List<Node> path = new List<Node>();

        void OnValidate()
        {
            if (null == start || null == goal)
            {
                path.Clear();
                return;
            }

            DoSearch();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            foreach (var node in path)
            {
                Gizmos.DrawWireSphere(node.transform.position, 0.2f);
            }
        }

        void DoSearch()
        {
            path.Clear();

            var limit = 2048;

            var opens = new List<Node>();
            var closes = new List<Node>();

            var costs = new Dictionary<Node, Cost>();

            costs.Add(start, new Cost());
            opens.Add(start);

            while (opens.Count > 0 && limit > 0)
            {
                limit--;

                var lowestF = GetLowestF(opens, costs);
//                print("Select: " + lowestF + " => " + goal);

                if (lowestF != goal)
                {
//                    print("Finding... : " + limit);
                    foreach (var neighbor in lowestF.neighbors)
                    {
                        if (!costs.ContainsKey(neighbor) && !closes.Contains(neighbor))
                        {
                            // F = G + H
                            var a = lowestF.transform.position;
                            var b = neighbor.transform.position;
                            var c = goal.transform.position;

                            var cost = new Cost();

                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Vector2.Distance(a, b);
                            cost.h = Vector2.Distance(b, c);
                            cost.f = cost.g + cost.h;

                            costs.Add(neighbor, cost);
                            opens.Add(neighbor);
                        }
                    }

                    closes.Add(lowestF);
                    opens.Remove(lowestF);
                }
                else
                {
                    var node = lowestF;
                    var cost = costs[lowestF];

                    do
                    {
                        path.Insert(0, node);

                        cost = costs[node];
                        node = cost.parent;
                    }
                    while(null != node);

                    // Ignore Limit
                    break;
                }
            }
        }

        Node GetLowestF(List<Node> opens, Dictionary<Node, Cost> costs)
        {
            return (from n in opens
                    orderby costs[n].f ascending
                    select n).FirstOrDefault();
        }

        class Cost
        {
            public Node parent;

            public float h;
            public float f;
            public float g;
        }
    }
}
