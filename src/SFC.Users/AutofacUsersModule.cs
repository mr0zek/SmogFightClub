using Autofac;
using SFC.Infrastructure;
using SFC.Users.Contract.Query;

namespace SFC.Users
{
  public class AutofacUsersModule : Module
  {
    private readonly string _connectionString;

    public AutofacUsersModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<UsersPerspective>()
        .AsImplementedInterfaces()
        .WithParameter("connectionString", _connectionString);

      builder.RegisterAssemblyTypes(GetType().Assembly)
           .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
           .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
