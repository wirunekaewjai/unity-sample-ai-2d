using System;

namespace Wirune.W03
{
    public interface IObserver
    {
        void OnNotify(object eventID, params object[] parameters);
    }
}

