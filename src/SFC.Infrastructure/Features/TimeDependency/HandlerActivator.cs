using Autofac;
using Microsoft.Extensions.Logging;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class HandlerActivator
  {
    private readonly ILifetimeScope _container;
    private readonly IDateTimeProvider _dateTimeProvider;

    public HandlerActivator(ILifetimeScope container, IDateTimeProvider dateTimeProvider)
    {
      _container = container;
      _dateTimeProvider = dateTimeProvider;
    }

    public async Task Run(Type type)
    {
      using (var scope = _container.BeginLifetimeScope())
      {
        IEnumerable<IEventHandlerAction<TimeEvent>> actions 
          = (IEnumerable<IEventHandlerAction<TimeEvent>>)scope.Resolve(typeof(IEnumerable<IEventHandlerAction<TimeEvent>>));

        TimeEventExecutionContext<TimeEvent> executionContext = scope.Resolve<TimeEventExecutionContext<TimeEvent>>();
        executionContext.Handler = (IEventHandler<TimeEvent>)scope.Resolve(type);

        TimeEvent @event = new TimeEvent(_dateTimeProvider.Now());

        foreach (var action in actions)
        {
          try
          {
            action.BeforeHandle(executionContext);
          }
          catch (Exception ex)
          {
            Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
          }
        }

        try
        {
          executionContext.Handler.Handle(@event);
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
