using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W03.Test02
{
    public class EnemyView : MonoBehaviour
    {
        public UnityAction CollisionEvent;
        public UnityAction ClickEvent;

        [SerializeField] private bool m_DrawDebugLine;
        [SerializeField] private Transform m_Transform;

        public Vector2 Position
        {
            get { return m_Transform.position; }
        }

        public void OnSeek(Vector2 target, float speed)
        {
            var displacement = target - Position;
            var direction = displacement.normalized;
            var velocity = direction * speed;

            m_Transform.Translate(velocity * Time.deltaTime, Space.World);
            m_Transform.up = direction;

            if (m_DrawDebugLine)
            {
                Debug.DrawLine(Position, target, Color.red);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Dispatcher.Invoke(CollisionEvent);
            }
        }

        private void OnMouseDown()
        {
            Dispatcher.Invoke(ClickEvent);
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

            m_Transform.position = position;
        }
    }
}
