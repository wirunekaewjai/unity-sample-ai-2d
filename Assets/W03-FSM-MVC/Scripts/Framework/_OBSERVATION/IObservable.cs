using System;

namespace Wirune.W03
{
    public interface IObservable
    {
        void Register(IObserver observer);
        void Unregister(IObserver observer);
        void Notify(object eventID, params object[] parameters);
    }
}

