using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public static class PathSmoother
    {
        public static void Smooth(List<Node> path)
        {
            for (int i = 0; i < path.Count - 2;)
            {
                Node n0 = path[i];
                Node n1 = path[i + 1];
                Node n2 = path[i + 2];

                Vector2 v0 = (n1.position - n0.position).normalized;
                Vector2 v1 = (n2.position - n1.position).normalized;

                if (Vector2.Dot(v0, v1) > 0)
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

