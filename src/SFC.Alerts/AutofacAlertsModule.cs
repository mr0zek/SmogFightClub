using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SFC.Alerts.Features.CreateAlert;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts
{
  public class AutofacAlertsModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AlertRepository>()
        .AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
