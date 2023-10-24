using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System.Diagnostics;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceEventHandlerAction<T> : IEventHandlerAction<T>
  where T : IEvent
  {
    private readonly ICallStack _callStack;

    public TraceEventHandlerAction(ICallStack callStack)
    {
      _callStack = callStack;
    }

    public void AfterHandle(IEventExecutionContext<T> eventExecutionContext)
    {
      _callStack.FinishCall("Return");
    }

    public void BeforeHandle(IEventExecutionContext<T> eventExecutionContext)
    {
      if (typeof(T) == typeof(TimeEvent))
      {
        _callStack.StartCall(
        eventExecutionContext.Handler.GetType().Assembly.GetName().Name,
        typeof(T).Name, "Event", "Time");
      }
      else
      {
        _callStack.StartCall(
        eventExecutionContext.Handler.GetType().Assembly.GetName().Name,
        typeof(T).Name, "Event");
      }
    }
  }
}
