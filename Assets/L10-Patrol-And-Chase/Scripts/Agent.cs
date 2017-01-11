using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.L10
{
    using Fsm = Wirune.L04.Fsm<Agent>;
    using Path = Wirune.L07.Path;
    using Point = Wirune.L07.Point;
    using Player = Wirune.L06.Player;
    using EyeVision = Wirune.L06.EyeVision;

    public class Agent : MonoBehaviour 
    {
        public const byte PATROL_STATE  = 1;
        public const byte OBSERVE_STATE = 2;
        public const byte CHASE_STATE   = 3;

        public bool drawGizmos = true;

        [Space]
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        public float radius = 0.5f;

        [Space]
        public EyeVision eyeVision;

        [Space]
        public float stoppingDistance = 0.1f;
        public Path path;

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        // Property
        public Fsm Fsm { get; private set; }
        public Player Player { get; private set; }

        // Non-Serialized
        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        void Awake()
        {
            eyeVision.onEnter.AddListener(OnObjectInSight);
            eyeVision.onExit.AddListener(OnObjectOutSight);

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
            if (!drawGizmos)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(Position, GetCurrentPoint().Position);

            if (null != Player)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(Position, Player.Position);
            }
        }

        // Copied from L05-Movement
        public Vector2 Seek(Vector2 target)
        {
            Vector2 displacement = (target - Position);
            Vector2 direction = displacement.normalized;

            return direction * moveSpeed * Time.deltaTime;
        }

        public void Rotate(Vector2 direction)
        {
            Quaternion lookAt = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeed);
        }
        //

        public Point GetCurrentPoint()
        {
            return path.GetPoint(m_CurrentPointIndex);
        }

        public void NextPoint()
        {
            if (path.Count <= 1)
            {
                m_CurrentPointIndex = 0;
                return;
            }

            if (m_IsForward)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = path.Count - 2;
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

        public void OnObjectInSight(Collider2D c)
        {
            if (c.tag == "Player")
            {
                Player = c.GetComponent<Player>();
            }
        }

        public void OnObjectOutSight(Collider2D c)
        {
            if (c.tag == "Player")
            {
                Player = null;
            }
        }
    }
}