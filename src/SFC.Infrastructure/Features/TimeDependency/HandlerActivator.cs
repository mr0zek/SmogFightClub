using Autofac;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
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
      IEventHandler<TimeEvent> handler = (IEventHandler<TimeEvent>)_componentContext.Resolve(type);
      handler.Handle(new TimeEvent(_dateTimeProvider.Now()));
    }
  }
}
