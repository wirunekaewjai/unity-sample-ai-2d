using UnityEngine;

namespace Wirune.W04.Test01
{
    public class Player : MvvmBehaviour
    {
        [SerializeField] private bool m_DrawDebugLine;

        [SerializeField, Range(0, 10)] private float m_Speed = 2f;
        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = Mathf.Clamp(value, 0f, 10f); }
        }

        private int m_Health = 3;
        public int Health
        {
            get { return m_Health; }
            set
            { 
                m_Health = Mathf.Clamp(value, 0, 10);
                Execute("Player.OnHealthChanged", m_Health);

                if (m_Health == 0)
                {
                    Execute("Player.OnDied");
                }
            }
        }

        private void Awake()
        {
            Health = 3;
            transform.position = Vector2.zero;
        }

        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            var velocity = new Vector2(h, v) * Speed;

            var mouseScreenPosition = Input.mousePosition;
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            Move(velocity);
            LookAt(mouseWorldPosition);
        }

        public void Move(Vector2 velocity)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        public void LookAt(Vector2 target)
        {
            var self = (Vector2)transform.position;
            var direction = (target - self);

            transform.up = direction;

            if (m_DrawDebugLine)
            {
                Debug.DrawLine(self, target, Color.red);
            }
        }


    }
}

