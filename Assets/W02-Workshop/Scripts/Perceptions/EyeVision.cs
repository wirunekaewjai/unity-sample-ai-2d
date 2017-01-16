using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W02
{
    public class EyeVision : MonoBehaviour
    {
        public bool drawGizmos = true;

        [Space]
        public float radius = 2f;
        public float angle = 45;
        public int division = 1;

        [Space]
        public Collider2DEvent onEnter;
        public Collider2DEvent onStay;
        public Collider2DEvent onExit;

        private List<Collider2D> m_Colliders = new List<Collider2D>();

        void Update()
        {
            Vector2 pos = transform.position;
            Vector2 up = transform.up;

            float half = angle / 2f;
            float step = angle / division;

            List<Collider2D> stayeds = new List<Collider2D>();
            List<Collider2D> exiteds = new List<Collider2D>(m_Colliders);

            for (float degree = -half; degree <= half; degree += step)
            {
                Vector2 p = pos + Rotate(up, degree) * radius;
                RaycastHit2D hit = Physics2D.Linecast(pos, p);
                Collider2D collider = hit.collider;

                if (null != collider)
                {
                    if (!m_Colliders.Contains(collider))
                    {
                        m_Colliders.Add(collider);
                        onEnter.Invoke(collider);
                    }
                    else 
                    {
                        exiteds.Remove(collider);

                        if (!stayeds.Contains(collider))
                        {
                            stayeds.Add(collider);
                            onStay.Invoke(collider);
                        }
                    }
                }
            }

            foreach (var exited in exiteds)
            {
                m_Colliders.Remove(exited);
                onExit.Invoke(exited);
            }

            exiteds.Clear();
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.grey;

            Vector2 pos = transform.position;
            Vector2 up = transform.up;

            float half = angle / 2f;
            float step = angle / division;

            for (float degree = -half; degree <= half; degree += step)
            {
                Vector2 p = pos + Rotate(up, degree) * radius;
                Gizmos.DrawLine(pos, p);
            }
        }

        public Vector2 Rotate(Vector2 v, float degree)
        {
            float radian = Mathf.Deg2Rad * degree;

            float x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
            float y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);

            return new Vector2(x, y);
        }
    }
}