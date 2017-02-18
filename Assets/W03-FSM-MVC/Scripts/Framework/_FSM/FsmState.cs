using System.Collections.Generic;
using System.Reflection;

namespace Wirune.W03
{
    public abstract class FsmState<T> : IObserver
    {
        public Fsm<T> Fsm { get; internal set; }
        public T Owner { get { return Fsm.Owner; } }

        public virtual void OnEnter()
        {
            // Fsm -> FsmState
            Fsm.Register(this);
        }

        public virtual void OnExit()
        {
            Fsm.Unregister(this);
        }

        #region IObserver implementation

        public void OnNotify(object eventID, params object[] parameters)
        {
            ObserveUtil.Invoke(this, eventID, parameters);
        }

        #endregion
    }
}