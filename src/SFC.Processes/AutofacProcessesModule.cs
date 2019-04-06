using Autofac;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Processes.Features.UserRegistration;

namespace SFC.Processes
{
  public class AutofacProcessesModule : Module
  {
    private readonly string _connectionString;

    public AutofacProcessesModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<PasswordHasher>().AsImplementedInterfaces();

      builder.RegisterType<SagaRepository>().AsImplementedInterfaces()
        .WithParameter("connectionString",_connectionString);

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
