using MediatR;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceEventHandlerBehavior<TEvent> : INotificationPipelineBehavior<TEvent>
    where TEvent : IEvent
  {
    ICallStack _callStack;

    public TraceEventHandlerBehavior(ICallStack callStack)
    {
      _callStack = callStack;
    }

    public async Task Handle(
      TEvent notification, 
      EventHandlerDelegate next, 
      INotificationHandler<TEvent> handler, 
      CancellationToken cancellationToken) 
    {
      var moduleName = handler.GetType().Assembly.GetName().Name;
      await _callStack.StartCall(moduleName, typeof(TEvent).Name, "Event");

      try
      {
        await next();
      }
      finally
      {
        await _callStack.FinishCall("return");        
      }
    }    
  }
}
