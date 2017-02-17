using System.Collections.Generic;
using System.Reflection;

namespace Wirune.W03
{
    public abstract class FsmState<T> : IObserver
    {
        private Dictionary<object, MethodInfo> m_Observes = new Dictionary<object, MethodInfo>();

        public Fsm<T> Fsm { get; internal set; }
        public T Owner { get { return Fsm.Owner; } }

        public FsmState()
        {
            // Find Observe Method Infos by 'ObserveAttribute'
            m_Observes = Observe.FindObserveMethods(this);
        }

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
            if (m_Observes.ContainsKey(eventID))
            {
                var method = m_Observes[eventID];
                method.Invoke(this, parameters);
            }
        }

        #endregion
    }
}