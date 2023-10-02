using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FluentValidation;
using SFC.Alerts.Infrastructure;
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

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
