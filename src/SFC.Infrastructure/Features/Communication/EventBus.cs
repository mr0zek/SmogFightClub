using System.Collections.Generic;
using System.Linq;
using Autofac;
using SFC.Infrastructure.Interfaces;

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
        foreach (var action in actions)
        {
          action.BeforeHandle(@event, eventHandler);
        }

        eventHandler.Handle(@event);

        foreach (var action in actions)
        {
          action.AfterHandle();
        }
      }
    }
  }
}
