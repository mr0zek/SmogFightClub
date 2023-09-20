﻿using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.SensorApi
{
  public class AutofacSensorApiModule : Module
  {
    private readonly string _connectionString;

    public AutofacSensorApiModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}