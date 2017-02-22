using System;

namespace Wirune.W04
{
    public interface IExecutable
    {
        // One-way Link
        void Register(IExecutable executable);
        void Unregister(IExecutable executable);

        // Two-way Link
        void Link(IExecutable executable);
        void Unlink(IExecutable executable);

        // Execute Command
        void Execute(object command, params object[] parameters);
    }
}

