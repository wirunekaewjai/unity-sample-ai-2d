using UnityEngine;

namespace Wirune.W04.Test01
{
    public class Enemy : MvvmBehaviour
    {
        [SerializeField] private bool m_DrawDebugLine;

        [SerializeField, Range(0, 10)] 
        private float m_Speed = 2f;

        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = Mathf.Clamp(value, 0f, 10f); }
        }

        private Transform m_Player;

        private void Awake()
        {
            m_Player = FindObjectOfType<Player>().transform;
        }

        private void Update()
        {
            if (null == m_Player)
                return;

            Seek(m_Player.transform.position, Speed);
        }

        private void Seek(Vector3 target, float speed)
        {
            var displacement = target - transform.position;
            var direction = displacement.normalized;
            var velocity = direction * speed;

            transform.Translate(velocity * Time.deltaTime, Space.World);
            transform.up = direction;

            if (m_DrawDebugLine)
            {
                Debug.DrawLine(transform.position, target, Color.red);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Execute("Enemy.OnCollision", this);
            }
        }

        private void OnMouseDown()
        {
            Execute("Enemy.OnClick", this);
        }

        public void ResetPosition()
        {
            var ratio = Screen.width / Screen.height;

            var camera = Camera.main;

            var height = camera.orthographicSize * 1.2f;
            var width = height * ratio;

            var randomWidth = 0f;
            var randomHeight = 0f;

            var choice = Random.Range(0, 100);

            if (choice < 25)
            {
                // Left
                randomWidth = -width;
                randomHeight = Random.Range(-height, height);
            }
            else if (choice < 50)
            {
                // Right
                randomWidth = width;
                randomHeight = Random.Range(-height, height);
            }
            else if (choice < 75)
            {
                // Bottom
                randomWidth = Random.Range(-width, width);
                randomHeight = -height;
            }
            else
            {
                // Top
                randomWidth = Random.Range(-width, width);
                randomHeight = height;
            }

            var center = (Vector2) camera.transform.position;
            var position = center + new Vector2(randomWidth, randomHeight);

            transform.position = position;
        }
    }
}
