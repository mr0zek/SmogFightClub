using System;

namespace SFC.Infrastructure.Interfaces.Communication
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}
