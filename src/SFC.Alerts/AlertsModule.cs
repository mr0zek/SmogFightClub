using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SFC.Alerts.Infrastructure;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Sensors;

namespace SFC.Alerts
{
  [ModuleDefinition("Backend")]
  public class AlertsModule : Module, IModule
  {
    public void ConfigureMvc(IMvcBuilder builder)
    {
    }

    public void ConfigureWebApplication(WebApplication app)
    {
    }
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
