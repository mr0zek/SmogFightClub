using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.Accounts
{
  public class AutofacAccountsModule : Module
  {
    private readonly string _connectionString;

    public AutofacAccountsModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AccountsRepository>()
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
