using System;

namespace SFC.Infrastructure
{
  public interface ICommandBus
  {
    void Send<T>(T command);
  }
}
