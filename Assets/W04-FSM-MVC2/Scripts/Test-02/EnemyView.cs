using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class EnemyView : MonoBehaviour
    {
        public UnityAction HitEvent;

        [SerializeField] private Transform m_Transform;

        public Vector2 Position
        {
            get { return m_Transform.position; }
        }

        public void Move(Vector2 target, float speed)
        {
            var displacement = target - Position;
            var direction = displacement.normalized;
            var velocity = direction * speed;

            m_Transform.Translate(velocity * Time.deltaTime, Space.World);
            m_Transform.up = direction;

//            Debug.DrawLine(Position, target, Color.red);
        }

        public void Hit()
        {
            Dispatcher.Invoke(HitEvent);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Hit();
            }
        }
    }
}
