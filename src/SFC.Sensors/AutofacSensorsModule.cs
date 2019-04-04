using Autofac;
using SFC.Infrastructure;
using SFC.Sensors.Features.RegisterMeasurement;

namespace SFC.Sensors
{
  public class AutofacSensorsModule : Module
  {
    private readonly string _connectionString;

    public AutofacSensorsModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SensorsRepository>().AsImplementedInterfaces();
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
