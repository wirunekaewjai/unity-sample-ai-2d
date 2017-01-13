using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L14
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

        public static void Smooth(List<Vector2> path, float radius)
        {
            for (int i = 0; i < path.Count - 2;)
            {
                Vector2 p0 = path[i];
                Vector2 p1 = path[i + 1];
                Vector2 p2 = path[i + 2];

                Vector2 v0 = (p1 - p0).normalized;
                Vector2 v1 = (p2 - p1).normalized;

                if (Vector2.Dot(v0, v1) >= 0.9f)
                {
                    path.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }

//            for (int i = 0; i < path.Count - 2;)
//            {
//                Vector2 p0 = path[i];
//                Vector2 p2 = path[i + 2];
//
//                Vector2 dir = (p2 - p0).normalized;
//                RaycastHit2D hit = Physics2D.CircleCast(p0, radius, dir);
//
//                if (null == hit.collider)
//                {
//                    path.RemoveAt(i + 1);
//                }
//                else
//                {
//                    i++;
//                }
//            }

        }
    }
}

