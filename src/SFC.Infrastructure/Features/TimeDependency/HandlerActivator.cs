using Autofac;
using Microsoft.Extensions.Logging;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.TimeDependency
{
    internal class HandlerActivator
  {
    private readonly IComponentContext _componentContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public HandlerActivator(IComponentContext componentContext, IDateTimeProvider dateTimeProvider)
    {
      _componentContext = componentContext;
      _dateTimeProvider = dateTimeProvider;
    }

    public async Task Run(Type type)
    {      
      IEnumerable<IEventHandlerAction<TimeEvent>> actions = (IEnumerable<IEventHandlerAction<TimeEvent>>)_componentContext.Resolve(typeof(IEnumerable<IEventHandlerAction<TimeEvent>>));

      IEventHandler<TimeEvent> handler = (IEventHandler<TimeEvent>)_componentContext.Resolve(type);

      TimeEvent @event = new TimeEvent(_dateTimeProvider.Now());

      foreach (var action in actions)
      {
        action.BeforeHandle(@event, handler);
      }

      handler.Handle(@event);

      foreach (var action in actions)
      {
        action.AfterHandle();
      }
    }
  }
}
