using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Infrastructure.Features.Communication
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