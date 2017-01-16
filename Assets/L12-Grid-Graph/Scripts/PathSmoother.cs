using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L12
{
    public static class PathSmoother
    {
        public static void Smooth(List<Node> path, float radius)
        {
            for (int i = 0; i < path.Count - 2;)
            {
                Node n0 = path[i];
                Node n2 = path[i + 2];

                Vector2 dir = (n2.position - n0.position).normalized;
                RaycastHit2D hit = Physics2D.CircleCast(n0.position, radius, dir);

                if (null == hit.collider)
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

