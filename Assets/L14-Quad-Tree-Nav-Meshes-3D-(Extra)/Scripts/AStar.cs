using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.L14
{
    public static class AStar 
    {
        class Cost
        {
            public Node parent;
            public Vector3 closest;

            public float h;
            public float f;
            public float g;
        }

        public static List<Vector3> Search(List<Node> nodes, Vector3 start, Vector3 goal, Heuristic heuristic, float radius, params GameObject[] excludes)
        {
            List<Vector3> path = new List<Vector3>();
            List<Node> opens = new List<Node>();

            Node a = Node.FindNearest(nodes, start);
            Node b = Node.FindNearest(nodes, goal);

            start = a.ClosestPoint(start);
            goal = b.ClosestPoint(goal);

            Dictionary<Node, Cost> costs = new Dictionary<Node, Cost>();

            costs.Add(a, new Cost() { closest = start });
            opens.Add(a);

            int limit = 65535;
            while (opens.Count > 0 && limit > 0)
            {
                limit--;

                Node lowestF = GetLowestF(opens, costs);

                opens.Remove(lowestF);

                if (lowestF != b)
                {
                    Vector3 p0 = costs[lowestF].closest;

                    int count = lowestF.GetNeighborCount();
                    for (int i = 0; i < count; i++)
                    {
                        int nodeIndex = lowestF.GetNeighbor(i);
                        Node neighbor = nodes[nodeIndex];

                        if (!costs.ContainsKey(neighbor))
                        {
                            Vector3 p1 = neighbor.ClosestPoint(p0);

                            Cost cost = new Cost();

                            cost.closest = p1;
                            cost.parent = lowestF;
                            cost.g = costs[lowestF].g + Distance(p0, p1, heuristic);
                            cost.h = Distance(p1, goal, heuristic);
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
                        cost = costs[node];
                        path.Insert(0, cost.closest);

                        node = cost.parent;
                    }
                    while(null != node);

                    path.Add(goal);

                    // End While-Loop
                    break;
                }
            }

            if (radius > Mathf.Epsilon)
            {
                Smooth(path, radius, excludes);
            }

            path.RemoveAt(0);

            return path;
        }

        private static Node GetLowestF(List<Node> opens, Dictionary<Node, Cost> costs)
        {
            IOrderedEnumerable<Node> nodes = (from n in opens
                orderby costs[n].f ascending
                select n);

            return nodes.First();
        }

        private static float Distance(Vector3 a, Vector3 b, Heuristic heuristic)
        {
            if (heuristic == Heuristic.Manhattan)
            {
                return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
            }

            // Euclidean
            return Vector3.Distance(a, b);
        }

        private static void Smooth(List<Vector3> path, float radius, params GameObject[] excludes)
        {
            for (int i = 0; i < path.Count - 2;)
            {
                Vector3 p0 = path[i];
                Vector3 p1 = path[i + 1];
                Vector3 p2 = path[i + 2];

                Vector3 disp = (p2 - p0);
                Vector3 dir = disp.normalized;

                p0 = p0 + (dir * radius);
                p2 = p2 - (dir * radius);

                float dist = disp.magnitude - radius;

                Collider[] colliders = Physics.OverlapCapsule(p0, p2, radius);
                bool overlap = false;

                foreach (var collider in colliders)
                {
                    GameObject obj = collider.gameObject;

                    if (excludes.Contains(obj))
                        continue;

                    if (obj.isStatic)
                    {
                        overlap = true;
                        break;
                    }
                }

                if(!overlap)
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
