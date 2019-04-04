using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace SFC.Infrastructure
{
  public class Bus : ICommandBus, IEventBus
  {
    private readonly IContainer _container;

    public Bus(IContainer container)
    {
      _container = container;
    }

    public void Send<T>(T command)
    {
      ICommandHandler<T> commandHandler = (ICommandHandler<T>)_container.Resolve(typeof(ICommandHandler<T>));
      commandHandler.Handle(command);
    }

    public void Publish<T>(T @event)
    {
      IEnumerable<IEventHandler<T>> eventHandlers =
        (IEnumerable<IEventHandler<T>>) _container.Resolve(typeof(IEnumerable<IEventHandler<T>>));

      foreach (var eventHandler in eventHandlers)
      {
        eventHandler.Handle(@event);
      }
    }
  }
}
