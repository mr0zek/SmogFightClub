using Autofac;
using FluentValidation;
using SFC.AdminApi.Features.AlertNotificationsWithUserData;
using SFC.AdminApi.Features.SearchableDashboard;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Sensors;

namespace SFC.AdminApi
{
  [ModuleDefinition("Api")]
  public class AdminApiModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<DashboardPerspective>().AsImplementedInterfaces();
      builder.RegisterType<SearchableDashboardPerspective>()
        .AsImplementedInterfaces();
      
      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
