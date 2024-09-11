using Autofac;
using FluentValidation;
using SFC.AdminApi.Features.AlertNotificationsWithUserData;
using SFC.AdminApi.Features.SearchableDashboard;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Modules;
using SFC.Sensors;

namespace SFC.AdminApi
{
  public class AdminApiModule : IHaveAutofacRegistrations, IModule
  {    

    public void RegisterTypes(ContainerBuilder builder)
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
