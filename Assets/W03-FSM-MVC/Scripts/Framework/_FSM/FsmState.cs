using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public abstract class FsmState<T>
    {
        private readonly Command m_Observer = new Command();

        public Fsm<T> Fsm { get; internal set; }
        public T Owner { get { return Fsm.Owner; } }

        public FsmState()
        {
            m_Observer.BindCallbackAttribute(this);
        }

        public virtual void OnEnter()
        {
            Fsm.Register(m_Observer);
        }

        public virtual void OnExit()
        {
            Fsm.Unregister(m_Observer);
        }
    }
}