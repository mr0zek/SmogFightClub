using Autofac;
using SFC.AdminApi.Features.Dashboard;
using SFC.AdminApi.Features.SearchableDashboard;
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
      builder.RegisterType<DashboardPerspective>().AsImplementedInterfaces();
      builder.RegisterType<SearchableDashboardPerspective>()
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
