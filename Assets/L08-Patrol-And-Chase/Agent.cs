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

        public Transform target;

        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float minDistance = 1f;
        public float maxDistance = 3f;

        public float stopDistance = 0.25f;

        // Non-Serialized
        public Wirune.L04.Fsm<Agent> fsm;

        [SerializeField]
        private Wirune.L07.Path path;

        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        void Awake()
        {
            fsm = new Wirune.L04.Fsm<Agent>(this);

            fsm.AddState(PATROL_STATE, new PatrolState());
            fsm.AddState(OBSERVE_STATE, new ObserveState());
            fsm.AddState(SEEK_STATE, new SeekState());
        }

        void OnEnable()
        {
            fsm.ChangeState(PATROL_STATE);
        }

        void OnDisable()
        {
            fsm.ChangeState(0);
        }

        void Update()
        {
            fsm.Update();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, minDistance);
            Gizmos.DrawWireSphere(transform.position, maxDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stopDistance);
        }

        public bool IsTargetInRange()
        {
            Vector2 displacement = target.position - transform.position;
            Vector2 direction = displacement.normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance);
            return hit.transform == target && hit.distance >= minDistance;
        }

        public void RotateTo(Vector2 direction)
        {
            float rotateSpeedPerFrame = rotateSpeed * Time.deltaTime;

            Vector2 stepDirection = Vector3.RotateTowards(transform.up, direction, rotateSpeedPerFrame, 0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, stepDirection);
        }

        public void MoveForward()
        {
            float moveSpeedPerFrame = moveSpeed * Time.deltaTime;
            Vector2 velocity = Vector2.up * moveSpeedPerFrame;

            transform.Translate(velocity);
        }

        public Vector2 GetCurrentPoint()
        {
            return path.GetPoint(m_CurrentPointIndex);
        }

        public void GoToNextPoint()
        {
            if (m_IsForward)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = path.Count - 1;
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
    }
}