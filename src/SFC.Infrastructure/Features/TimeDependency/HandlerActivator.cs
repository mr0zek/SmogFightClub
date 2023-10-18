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

      EventExecutionContext<TimeEvent> executionContext = new EventExecutionContext<TimeEvent>();
      executionContext.Handler = (IEventHandler<TimeEvent>)_componentContext.Resolve(type);

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
      catch(Exception ex)
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
