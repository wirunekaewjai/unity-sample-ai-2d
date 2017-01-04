using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L08
{
    public class Agent : MonoBehaviour 
    {
        public const byte PATROL_STATE  = 1;
        public const byte OBSERVE_STATE = 2;
        public const byte SEEK_STATE    = 3;

//        public Transform target;
//
//        public float moveSpeed = 2f;
//        public float rotateSpeed = 5f;
//
//        public float minDistance = 1f;
//        public float maxDistance = 3f;
//
//        public float stopDistance = 0.25f;

        [SerializeField]
        private Wirune.L05.Player m_Player;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private float m_MinDistance = 1f;

        [SerializeField]
        private float m_MaxDistance = 3f;

        [SerializeField]
        private Wirune.L07.Path m_Path;

        // Non-Serialized
        private Wirune.L04.Fsm<Agent> m_Fsm;

        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        void OnValidate()
        {
            m_MaxDistance = Mathf.Clamp(m_MaxDistance, 0.15f, 100f);
            m_MinDistance = Mathf.Clamp(m_MinDistance, 0.1f, m_MaxDistance);
        }

        void Awake()
        {
            m_Fsm = new Wirune.L04.Fsm<Agent>(this);

            m_Fsm.AddState(PATROL_STATE, new PatrolState());
            m_Fsm.AddState(OBSERVE_STATE, new ObserveState());
            m_Fsm.AddState(SEEK_STATE, new SeekState());
        }

        void OnEnable()
        {
            m_Fsm.ChangeState(PATROL_STATE);
        }

        void OnDisable()
        {
            m_Fsm.ChangeState(0);
        }

        void Update()
        {
            m_Fsm.Update();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_MinDistance);
            Gizmos.DrawWireSphere(transform.position, m_MaxDistance);
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateSpeedPerFrame = m_RotateSpeed * Time.deltaTime;
            Vector2 newDirection = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);

            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
        }

        public void MoveForward(float distance)
        {
            float moveSpeedPerFrame = Mathf.Min(m_MoveSpeed * Time.deltaTime, distance);
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }

        public Vector2 GetCurrentPoint()
        {
            return m_Path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            if (m_IsForward)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= m_Path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = m_Path.Count - 1;
                }
            }
            else
            {
                m_CurrentPointIndex--;

                if (m_CurrentPointIndex < 0)
                {
                    m_IsForward = true;
                    m_CurrentPointIndex = 0;
                }
            }
        }

        public Wirune.L04.Fsm<Agent> GetFsm()
        {
            return m_Fsm;
        }

        public bool HasPlayer()
        {
            Vector2 displacement = m_Player.transform.position - transform.position;
            float distance = displacement.magnitude - m_Player.radius;

            return distance <= m_MaxDistance;
        }

        public Wirune.L05.Player GetPlayer()
        {
            return m_Player;
        }
    }
}