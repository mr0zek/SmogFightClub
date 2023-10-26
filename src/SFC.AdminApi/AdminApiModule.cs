using Autofac;
using FluentValidation;
using SFC.AdminApi.Features.AlertNotificationsWithUserData;
using SFC.AdminApi.Features.SearchableDashboard;
using SFC.Infrastructure;
using SFC.Infrastructure.Features.Modules;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Sensors;

namespace SFC.AdminApi
{
  [ModuleDefinition("Api")]
  public class AdminApiModule : Module, IHaveWorker, IModule
  {
    IEventAsyncProcessor _eventAsyncProcessor;
    public void StartWorker(IComponentContext container)
    {
      _eventAsyncProcessor = container.Resolve<IEventAsyncProcessor>();
      _eventAsyncProcessor.Start("AdminApi");
    }

    public void StopWorker()
    {
      _eventAsyncProcessor?.Stop();
    }

    public void WaitForShutdown()
    {
      _eventAsyncProcessor.WaitForShutdown();
    }

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
