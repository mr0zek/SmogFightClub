using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Sensors.Features.SensorQuery;

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
      builder.RegisterType<SensorsRepository>().WithParameter("connectionString", _connectionString).AsImplementedInterfaces();

      builder.RegisterType<MeasurementRepository>().WithParameter("connectionString", _connectionString).AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
