//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//namespace Test.POV
//{
//    public class FloodFill : MonoBehaviour 
//    {
//        public int gridSize;
//        public int cellSize;
//        public Vector2 start;
//
//        private List<Vector2[]> lines;
//        private List<Vector2> graphs;
//        private Queue<Vector2> queues;
//
//        private Vector2[] directions = 
//            {
////                new Vector2(-1, -1),
////                new Vector2(-1, 1),
////                new Vector2(1, 1),
////                new Vector2(1, -1),
//
//                new Vector2(-1, 0),
//                new Vector2(0, 1),
//                new Vector2(1, 0),
//                new Vector2(0, -1),
//            };
//
//        IEnumerator Start () 
//        {
//            lines = new List<Vector2[]>();
//            graphs = new List<Vector2>();
//            queues = new Queue<Vector2>();
//
//            Vector2 p = new Vector2(start.x, start.y);
//            queues.Enqueue(p);
//
//
//            while(queues.Count > 0)
//            {
//                Queue<Vector2> nextQueues = new Queue<Vector2>();
//                Vector2 q = queues.Dequeue();
//
//                Flood(nextQueues, q);
//                queues = nextQueues;
//
//                yield return null;
//            }
//
//        }
//
//        void OnDrawGizmos()
//        {
//            if (null == lines || lines.Count == 0)
//                return;
//
//            Gizmos.color = Color.yellow;
//
//            foreach (Vector2[] line in lines)
//            {
//                Gizmos.DrawLine(line[0], line[1]);
//            }
//        }
//
//        void Flood(Queue<Vector2> queues, Vector2 p)
//        {
//            int half = (int)(gridSize / 2f);
//            bool bx = (p.x < -half || p.x >= half);
//            bool bz = (p.y < -half || p.y >= half);
//
//            if(bx || bz || graphs.Contains(p))
//                return;
//
//            graphs.Add(p);
//
//            foreach(Vector2 dir in directions)
//            {
//                Vector2 d = dir * cellSize;
//                Vector2 p1 = p + d;
//
//                if(Physics2D.Linecast(p, p1) == false)
//                {
//                    queues.Enqueue(p1);
//                    lines.Add(new Vector2[] { p, p1 });
//                }
//            }
//        }
//
//
//    }
//}
