using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W01
{
    public class Character : MonoBehaviour 
    {
        private const int c_RotateSpeedMultiplier = 50;

//        [SerializeField]
//        private float m_Radius = 0.5f;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 10f;

        public float MoveSpeed
        {
            get
            {
                return m_MoveSpeed;
            }
            set
            {
                m_MoveSpeed = Mathf.Clamp(value, 0.2f, 100);
            }
        }

        public float RotateSpeed
        {
            get
            {
                return m_RotateSpeed;
            }
            set
            {
                m_RotateSpeed = Mathf.Clamp(m_RotateSpeed, 1, 360);
            }
        }

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
        }

        protected virtual void OnValidate()
        {
            m_MoveSpeed = Mathf.Clamp(m_MoveSpeed, 0.2f, 100);
            m_RotateSpeed = Mathf.Clamp(m_RotateSpeed, 1, 360);
        }

        protected virtual void OnDrawGizmos()
        {
            
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateSpeedPerFrame = m_RotateSpeed * Time.deltaTime * c_RotateSpeedMultiplier;

            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeedPerFrame);
        }

        public void MoveTo(Vector2 direction, float maxDistance)
        {
            float moveSpeedPerFrame = Mathf.Min(m_MoveSpeed * Time.deltaTime, maxDistance);
            Vector2 velocity = direction * moveSpeedPerFrame;

            transform.Translate(velocity, Space.World);
        }
    }
}
