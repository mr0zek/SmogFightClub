using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.UserApi
{
  public class AutofacUserApiModule : Module
  {
    private readonly string _connectionString;

    public AutofacUserApiModule(string connectionString)
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
