using System;

namespace Wirune.W03
{
    public interface ICommander
    {
        void Register(ICommand observer);
        void Unregister(ICommand observer);
        void Execute(object command, params object[] parameters);
    }
}

