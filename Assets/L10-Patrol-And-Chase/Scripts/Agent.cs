using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using Fsm = Wirune.L04.Fsm<Agent>;
    using Path = Wirune.L07.Path;
    using Point = Wirune.L07.Point;

    public class Agent : MonoBehaviour 
    {
        public const byte PATROL_STATE  = 1;
        public const byte OBSERVE_STATE = 2;
        public const byte CHASE_STATE   = 3;

        [SerializeField]
        private float m_MoveSpeed = 2f;

        [SerializeField]
        private float m_RotateSpeed = 5f;

        [SerializeField]
        private float m_StopDistance = 0.75f;

        [SerializeField]
        private Path m_Path;

        // Non-Serialized
        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        public CircleCollider2D Player { get; private set; }
        public Fsm Fsm { get; private set; }

        public float StopDistance
        {
            get { return m_StopDistance; }
        }

        void Awake()
        {
            Fsm = new Fsm(this);

            Fsm.AddState(PATROL_STATE, new PatrolState());
            Fsm.AddState(OBSERVE_STATE, new ObserveState());
            Fsm.AddState(CHASE_STATE, new ChaseState());
        }

        void OnEnable()
        {
            Fsm.ChangeState(PATROL_STATE);
        }

        void OnDisable()
        {
            Fsm.ChangeState(0);
        }

        void Update()
        {
            Fsm.Update();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_StopDistance);
        }

        public void RotateTo(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, m_RotateSpeed);
        }

        public void MoveForward(float distance)
        {
            float moveSpeedPerFrame = Mathf.Min(m_MoveSpeed * Time.deltaTime, distance);
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }

        public Point GetCurrentPoint()
        {
            return m_Path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            if (m_Path.Count <= 1)
            {
                m_CurrentPointIndex = 0;
                return;
            }

            if (m_IsForward)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= m_Path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = m_Path.Count - 2;
                }
            }
            else
            {
                m_CurrentPointIndex--;

                if (m_CurrentPointIndex < 0)
                {
                    m_IsForward = true;
                    m_CurrentPointIndex = 1;
                }
            }
        }

        public void OnPlayerEnter(Collider2D player)
        {
            Player = player.GetComponent<CircleCollider2D>();
        }

        public void OnPlayerExit(Collider2D player)
        {
            Player = null;
        }
    }
}