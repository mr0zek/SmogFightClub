using Hangfire;
using System;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class ContainerJobActivator : JobActivator
  {
    private IServiceProvider _container;

    public ContainerJobActivator(IServiceProvider container)
    {
      _container = container;
    }

    public override object ActivateJob(Type type)
    {
      return _container.GetService(type);
    }
  }
}