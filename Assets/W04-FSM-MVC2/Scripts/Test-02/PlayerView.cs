using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class PlayerView : MonoBehaviour
    {
        public UnityAction HitEvent;

        [SerializeField] private Transform m_Transform;

        public Vector2 Position
        {
            get { return m_Transform.position; }
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            m_Transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        public void UpdateLookAt(Vector2 target)
        {
            var self = (Vector2)m_Transform.position;
            var direction = (target - self);

            m_Transform.up = direction;
//            Debug.DrawLine(self, target, Color.red);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Respawn")
            {
                Dispatcher.Invoke(HitEvent);
            }
        }
    }
}
