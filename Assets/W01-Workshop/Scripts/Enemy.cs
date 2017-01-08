using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W01
{
    public class Enemy : Character 
    {
        public const byte PATROL_STATE  = 1;
        public const byte OBSERVE_STATE = 2;
        public const byte CHASE_STATE   = 3;

        [SerializeField]
        private float m_RunSpeed = 1.75f;

        [SerializeField]
        private float m_StopDistance = 0.75f;

        [SerializeField]
        private PatrolPath m_Path;

        // Non-Serialized
        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        public Player Player { get; private set; }
        public Fsm<Enemy> Fsm { get; private set; }

        public float RunSpeed
        {
            get
            {
                return m_RunSpeed;
            }
            set
            {
                m_RunSpeed = Mathf.Clamp(value, 0.2f, 100);
            }
        }

        public float StopDistance
        {
            get
            {
                return m_StopDistance;
            }
            set
            {
                m_StopDistance = Mathf.Clamp(value, 0.2f, 100);
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            m_RunSpeed = Mathf.Clamp(m_RunSpeed, 0.2f, 100);
            m_StopDistance = Mathf.Clamp(m_StopDistance, 0.2f, 100);
        }

        protected virtual void Awake()
        {
            Fsm = new Fsm<Enemy>(this);

            Fsm.AddState(PATROL_STATE, new PatrolState());
            Fsm.AddState(OBSERVE_STATE, new ObserveState());
            Fsm.AddState(CHASE_STATE, new ChaseState());
        }

        protected virtual void OnEnable()
        {
            Fsm.ChangeState(PATROL_STATE);
        }

        protected virtual void OnDisable()
        {
            Fsm.ChangeState(0);
        }

        protected virtual void Update()
        {
            Fsm.Update();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_StopDistance);

            if (null != Fsm)
            {
                Fsm.DrawGizmos();
            }
        }

        public PatrolPoint GetCurrentPoint()
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

        public Queue<Vector2> GenerateSubPath(Node goal)
        {
            Node start = Object.FindObjectOfType<FloodFill>().FindNearest(Position);
            return GenerateSubPath(start, goal);
        }

        public Queue<Vector2> GenerateSubPath(Node start, Node goal)
        {
            Queue<Vector2> subPath = new Queue<Vector2>();

            List<Node> nodes = AStar.Search(start, goal, Heuristic.Euclidean);
            PathSmoother.Smooth(nodes, 0.5f);

            foreach (var node in nodes)
            {
                subPath.Enqueue(node.position);
            }

            return subPath;
        }

        // Invoked from Eye Perception
        public void OnPlayerEnter(Perception sender, Collider2D player)
        {
            Player = player.GetComponent<Player>();

            Vector2 origin = Position;
            Vector2 displacement = (Player.Position - origin);
            Vector2 direction = displacement.normalized;

            float distance = displacement.magnitude;
            float radius = Player.Radius;

            RaycastHit2D[] hits = Physics2D.LinecastAll(origin, Player.Position);
            hits = (from hit in hits
                             orderby hit.distance ascending
                             where hit.transform != player.transform
                             where hit.transform != sender.transform
                             select hit).ToArray();

            // Has Obstacle In Front of Player
            if (hits.Length > 0)
            {
//                print("F: "+hits[0].transform);
                Player = null;
            }
        }

        public void OnPlayerExit(Perception sender, Collider2D player)
        {
            Player = null;
        }
    }
}
