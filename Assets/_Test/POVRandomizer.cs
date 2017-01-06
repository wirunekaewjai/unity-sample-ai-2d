using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test.POV
{
    public class POVRandomizer : MonoBehaviour
    {
        public int min = 10;
        public int max = 20;

        public float multiplier = 0.5f;

        IEnumerator Start()
        {
            int size = Random.Range(min, max + 1);
            Node[,] nodes = new Node[size, size];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float r = Random.Range(-1f, 1f);

//                    if (r >= 0)
                    {
                        Node n = Create(x * multiplier, y * multiplier, size * multiplier);
                        nodes[x, y] = n;

                        if (x - 1 >= 0 && null != nodes[x - 1, y])
                        {
                            Node left = nodes[x - 1, y];
                            n.neighbors.Add(left);
                            left.neighbors.Add(n);
                        }

                        if (y - 1 >= 0 && null != nodes[x, y - 1])
                        {
                            Node bottom = nodes[x, y - 1];
                            n.neighbors.Add(bottom);
                            bottom.neighbors.Add(n);
                        }
                    }

                }
            }



            yield return null;


        }

        Node Create(float x, float y, float size)
        {
            float px = x - (size / 2f) + 0.125f;
            float py = y - (size / 2f) + 0.125f;

            GameObject g = new GameObject("Node [" + x + ", " + y + "]");
            Node n = g.AddComponent<Node>();

            g.transform.position = new Vector3(px, py, 0);
            g.transform.SetParent(transform);

            return n;
        }
    }
}

