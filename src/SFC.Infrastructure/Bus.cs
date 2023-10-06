using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
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

    public void Send<T>(T command) where T : ICommand  
    {
      ICommandHandler<T> commandHandler = (ICommandHandler<T>)_container.Resolve(typeof(ICommandHandler<T>));
      commandHandler.Handle(command);
    }

    public void Publish<T>(T @event) where T : IEvent
    {
      IEnumerable<IEventHandler<T>> eventHandlers =
        _container.Resolve<IEnumerable<IEventHandler<T>>>().DistinctBy(f=>f.GetType());

      foreach (var eventHandler in eventHandlers)
      {
        eventHandler.Handle(@event);
      }
    }

    public TResponse Query<TResponse>(IRequest<TResponse> request)where TResponse : IResponse  
    {
      Type generic = typeof(IQueryHandler<,>);
      generic = generic.MakeGenericType(request.GetType(), typeof(TResponse));

      var queryHandler = _container.Resolve(generic);
      return (TResponse)queryHandler.GetType().InvokeMember("HandleQuery",System.Reflection.BindingFlags.InvokeMethod,null,queryHandler, new[] { request });
    }
  }
}
