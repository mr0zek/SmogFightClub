using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using static System.Collections.Specialized.BitVector32;

namespace SFC.Infrastructure.Features.Communication
{
  class EventBus : IEventBus
  {
    private readonly IComponentContext _container;

    public EventBus(IComponentContext container)
    {
      _container = container;
    }

    public void Publish<T>(T @event) where T : IEvent
    {
      IEnumerable<IEventHandlerAction<T>> actions = (IEnumerable<IEventHandlerAction<T>>)_container.Resolve(typeof(IEnumerable<IEventHandlerAction<T>>));

      IEnumerable<IEventHandler<T>> eventHandlers =
        _container.Resolve<IEnumerable<IEventHandler<T>>>().DistinctBy(f => f.GetType());

      foreach (var eventHandler in eventHandlers)
      {
        EventExecutionContext<T> executionContext = new EventExecutionContext<T>()
        {
          Handler = eventHandler,
          Event = @event
        };

        foreach (var action in actions)
        {
          try
          {
            action.BeforeHandle(executionContext);
          }
          catch (Exception ex)
          {
            Log.Error(ex, "Exception while processing BeforeHandle of action : {action}", action.GetType().Name);
          }
        }

        try
        {
          eventHandler.Handle(@event);
        }
        catch (Exception ex)
        {
          executionContext.Exception = ex;
        }

        foreach (var action in actions)
        {
          try
          {
            action.AfterHandle(executionContext);
          }
          catch (Exception ex)
          {
            Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
          }
        }
      }
    }

    
  }
}
