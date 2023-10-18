using System;

namespace SFC.Infrastructure.Interfaces.Communication
{
  class EventExecutionContext<T> : IEventExecutionContext<T>
    where T : IEvent
  {
    public T Event { get; set; }

    public Exception Exception { get; set; }

    public IEventHandler<T> Handler
    {
      get; set;
    }
  }
}