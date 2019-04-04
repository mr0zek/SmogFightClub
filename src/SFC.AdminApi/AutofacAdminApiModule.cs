using Autofac;
using SFC.Infrastructure;

namespace SFC.AdminApi
{
  public class AutofacAdminApiModule : Module
  {
    private readonly string _connectionString;

    public AutofacAdminApiModule(string connectionString)
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
