﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wirune.W02
{
    public class Enemy : MonoBehaviour 
    {
        public const byte PATROL_STATE  = 1;
        public const byte OBSERVE_STATE = 2;
        public const byte CHASE_STATE   = 3;

        [Space]
        public GraphAgent agent;
        public PatrolPath path;
        public EyeVision eyeVision;

        [Space]
        public float runSpeed = 1.75f;
        public bool patrolLoop = true;

        // Non-Serialized
        private bool m_IsForward = true;
        private int m_CurrentPointIndex = 0;

        public Player Player { get; private set; }
        public Fsm<Enemy> Fsm { get; private set; }

        void Awake()
        {
            eyeVision.onEnter.AddListener(OnObjectInSight);
            eyeVision.onExit.AddListener(OnObjectOutSight);

            Fsm = new Fsm<Enemy>(this);

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

        void FixedUpdate()
        {
            Fsm.Update();
        }

        public PatrolPoint GetCurrentPoint()
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

            if (patrolLoop)
            {
                m_CurrentPointIndex++;

                if (m_CurrentPointIndex >= path.Count)
                {
                    m_IsForward = false;
                    m_CurrentPointIndex = 0;
                }

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
