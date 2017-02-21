using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Wirune.W04.Test02
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private bool m_DrawDebugLine;
        [SerializeField] private Transform m_Transform;

        public Vector2 Position
        {
            get { return m_Transform.position; }
        }

        public void OnVelocityChanged(Vector2 velocity)
        {
            m_Transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        public void OnTargetChanged(Vector2 target)
        {
            var self = (Vector2)m_Transform.position;
            var direction = (target - self);

            m_Transform.up = direction;

            if (m_DrawDebugLine)
            {
                Debug.DrawLine(self, target, Color.red);
            }
        }

        private void OnEnable()
        {
            m_Transform.position = Vector2.zero;
        }
    }
}
