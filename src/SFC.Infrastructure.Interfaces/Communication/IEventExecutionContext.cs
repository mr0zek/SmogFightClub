using System;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventExecutionContext<T>
    where T : IEvent
  {
    T Event { get; }

    Exception Exception { get; set; }

    IEventHandler<T> Handler { get; }
  }
}