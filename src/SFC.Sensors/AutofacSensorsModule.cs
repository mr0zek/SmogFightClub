using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Sensors.Infratructure;

namespace SFC.Sensors
{
    public class AutofacSensorsModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SensorRepository>().AsImplementedInterfaces();

      builder.RegisterType<MeasurementRepository>().AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
