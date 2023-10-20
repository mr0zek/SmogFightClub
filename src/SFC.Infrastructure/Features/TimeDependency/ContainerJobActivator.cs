using Autofac;
using Hangfire;
using System;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class ContainerJobActivator : JobActivator
  {
    private IComponentContext _container;

    public ContainerJobActivator(IComponentContext container)
    {
      _container = container;
    }

    public override object ActivateJob(Type type)
    {
      return _container.Resolve(type);
    }
  }
}