using System.Collections.Generic;
using System.Reflection;
using System;

using UnityEngine;
using UnityEngine.Events;

namespace Wirune.W04
{
    public class FsmUpdatableState<TOwner> : FsmState<TOwner>
    {
        private static readonly BindingFlags c_Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private UnityAction m_Update;
        private UnityAction m_FixedUpdate;
        private UnityAction m_LateUpdate;

        private UnityAction GetAction(string method)
        {
            var methodInfo = GetType().GetMethod(method, c_Flags);

            if (null != methodInfo)
            {
                var action = (Action)Delegate.CreateDelegate(typeof(Action), this, method);
                return new UnityAction(action);
            }

            return null;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            m_Update = GetAction("Update");
            m_FixedUpdate = GetAction("FixedUpdate");
            m_LateUpdate = GetAction("LateUpdate");

            if (null == m_Update)
            {
                m_Update = GetAction("OnUpdate");
            }

            if (null == m_FixedUpdate)
            {
                m_FixedUpdate = GetAction("OnFixedUpdate");
            }

            if (null == m_LateUpdate)
            {
                m_LateUpdate = GetAction("OnLateUpdate");
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (null != m_Update)
            {
                Looper.RegisterUpdate(m_Update);
            }

            if (null != m_FixedUpdate)
            {
                Looper.RegisterFixedUpdate(m_FixedUpdate);
            }

            if (null != m_LateUpdate)
            {
                Looper.RegisterLateUpdate(m_LateUpdate);
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (null != m_Update)
            {
                Looper.UnregisterUpdate(m_Update);
            }

            if (null != m_FixedUpdate)
            {
                Looper.UnregisterFixedUpdate(m_FixedUpdate);
            }

            if (null != m_LateUpdate)
            {
                Looper.UnregisterLateUpdate(m_LateUpdate);
            }
        }
    }
}

