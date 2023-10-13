using Autofac;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.TimeDependency
{
  internal class HandlerActivator
  {
    private readonly IComponentContext _componentContext;

    public HandlerActivator(IComponentContext componentContext)
    {
      _componentContext = componentContext;
    }

    public async Task Run(Type type)
    {
      IEventHandler<TimeEvent> handler = (IEventHandler<TimeEvent>)_componentContext.Resolve(type);
      handler.Handle(new TimeEvent());
    }
  }
}
