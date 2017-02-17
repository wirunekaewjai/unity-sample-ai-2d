using System;

namespace Wirune.W03
{
    public interface ICommander
    {
        void Register(ICommand command);
        void Unregister(ICommand command);
        void Execute(object id, params object[] parameters);
    }
}

