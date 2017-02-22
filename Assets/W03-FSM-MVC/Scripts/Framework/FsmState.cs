using UnityEngine;

namespace Wirune.W03
{
    public class FsmState<TOwner>
    {
        public Fsm<TOwner> Fsm { get; internal set; }
        public TOwner Owner { get { return Fsm.Owner; } }

        public virtual void OnCreate()
        {

        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}

