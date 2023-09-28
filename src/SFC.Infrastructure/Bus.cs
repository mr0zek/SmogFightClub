using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using SFC.Infrastructure.Interfaces;

namespace SFC.Infrastructure
{
  public class Bus : ICommandBus, IEventBus, IQuery
  {
    private readonly IComponentContext _container;

    public Bus(IComponentContext container)
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

    public TResult Query<TResult>(IRequest<TResult> request)
    {
      Type generic = typeof(IQueryHandler<,>);
      generic = generic.MakeGenericType(request.GetType(), typeof(TResult));

      var queryHandler = _container.Resolve(generic);
      return (TResult)queryHandler.GetType().InvokeMember("HandleQuery",System.Reflection.BindingFlags.InvokeMethod,null,queryHandler, new[] { request });
    }
  }
}
