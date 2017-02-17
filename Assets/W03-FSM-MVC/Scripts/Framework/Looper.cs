using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Wirune.W03
{
    public sealed class Looper : MonoBehaviour
    {
        private Action m_UpdateCallback;
        private Action m_FixedUpdateCallback;
        private Action m_LateUpdateCallback;

        private void Update()
        {
            if (null != m_UpdateCallback)
            {
                m_UpdateCallback.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (null != m_FixedUpdateCallback)
            {
                m_FixedUpdateCallback.Invoke();
            }
        }

        private void LateUpdate()
        {
            if (null != m_LateUpdateCallback)
            {
                m_LateUpdateCallback.Invoke();
            }
        }

        public static void RegisterUpdate(Action action)
        {
            Instance.m_UpdateCallback += action;
        }

        public static void UnregisterUpdate(Action action)
        {
            Instance.m_UpdateCallback -= action;
        }

        public static void RegisterFixedUpdate(Action action)
        {
            Instance.m_FixedUpdateCallback += action;
        }

        public static void UnregisterFixedUpdate(Action action)
        {
            Instance.m_FixedUpdateCallback -= action;
        }

        public static void RegisterLateUpdate(Action action)
        {
            Instance.m_LateUpdateCallback += action;
        }

        public static void UnregisterLateUpdate(Action action)
        {
            Instance.m_LateUpdateCallback -= action;
        }

        private static Looper s_Instance = null;
        private static Looper Instance
        {
            get
            {
                if (null == s_Instance)
                {
                    s_Instance = FindObjectOfType<Looper>();
                }

                if (null == s_Instance)
                {
                    GameObject g = new GameObject("FSM Manager");

                    s_Instance = g.AddComponent<Looper>();
                    s_Instance.transform.SetSiblingIndex(0);
                }

                return s_Instance;
            }
        }
    }
}