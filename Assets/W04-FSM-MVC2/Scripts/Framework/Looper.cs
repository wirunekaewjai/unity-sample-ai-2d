using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04
{
    public sealed class Looper : MonoBehaviour
    {
        private UnityAction m_UpdateCallback;
        private UnityAction m_FixedUpdateCallback;
        private UnityAction m_LateUpdateCallback;

        private void Update()
        {
            Dispatcher.Invoke(m_UpdateCallback);
        }

        private void FixedUpdate()
        {
            Dispatcher.Invoke(m_FixedUpdateCallback);
        }

        private void LateUpdate()
        {
            Dispatcher.Invoke(m_LateUpdateCallback);
        }

        public static void RegisterUpdate(UnityAction action)
        {
            Instance.m_UpdateCallback += action;
        }

        public static void UnregisterUpdate(UnityAction action)
        {
            if (null != s_Instance)
            {
                s_Instance.m_UpdateCallback -= action;
            }
        }

        public static void RegisterFixedUpdate(UnityAction action)
        {
            Instance.m_FixedUpdateCallback += action;
        }

        public static void UnregisterFixedUpdate(UnityAction action)
        {
            if (null != s_Instance)
            {
                s_Instance.m_UpdateCallback -= action;
            }
        }

        public static void RegisterLateUpdate(UnityAction action)
        {
            Instance.m_LateUpdateCallback += action;
        }

        public static void UnregisterLateUpdate(UnityAction action)
        {
            if (null != s_Instance)
            {
                s_Instance.m_LateUpdateCallback -= action;
            }
        }

        public static Coroutine RegisterCoroutine(System.Collections.IEnumerator enumerator)
        {
            return Instance.StartCoroutine(enumerator);
        }

        public static void UnregisterCoroutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
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
                    GameObject g = new GameObject("Looper");

                    s_Instance = g.AddComponent<Looper>();
                    s_Instance.transform.SetSiblingIndex(0);
                }

                return s_Instance;
            }
        }
    }
}