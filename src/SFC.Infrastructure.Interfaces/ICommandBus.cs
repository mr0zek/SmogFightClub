using System;

namespace SFC.Infrastructure.Interfaces
{
  public interface ICommandBus
  {
    void Send<T>(T command) where T : ICommand;
  }
}
