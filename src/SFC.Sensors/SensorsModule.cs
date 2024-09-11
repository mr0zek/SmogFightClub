using Autofac;
using FluentValidation;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Modules;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Sensors.Infrastructure;

namespace SFC.Sensors
{
  public class SensorsModule : IHaveAutofacRegistrations, IModule
  {
    public void RegisterTypes(ContainerBuilder builder)
    {
      builder.RegisterType<SensorRepository>().AsImplementedInterfaces();

      builder.RegisterType<MeasurementRepository>().AsImplementedInterfaces();

      builder.RegisterType<PM25Verificator>().AsImplementedInterfaces();

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
