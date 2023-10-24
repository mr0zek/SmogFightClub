using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class TimeEventExecutionContext<T> : IEventExecutionContext<TimeEvent>
  {
    public TimeEvent Event { get; set; }

    public Exception Exception { get; set; }

    public IEventHandler<TimeEvent> Handler
    {
      get; set;
    }
  }
}