using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.W03
{
    public abstract class FsmState<T>
    {
        private readonly Command m_Command = new Command();

        public Fsm<T> Fsm { get; internal set; }
        public T Owner { get { return Fsm.Owner; } }

        public FsmState()
        {
            m_Command.BindCallbackAttribute(this);
        }

        public virtual void OnEnter()
        {
            Fsm.Register(m_Command);
        }

        public virtual void OnExit()
        {
            Fsm.Unregister(m_Command);
        }
    }
}