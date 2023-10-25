using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Processes.Features.UserRegistration;
using SFC.Processes.Features.UserRegistrationSaga;
using SFC.Sensors;

namespace SFC.Processes
{
  [ModuleDefinition("Task")]
  public class ProcessesModule : Module, IModule
  {
    public void ConfigureMvc(IMvcBuilder builder)
    {
    }

    public void ConfigureWebApplication(WebApplication app)
    {
    }
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<SagaRepository>().AsImplementedInterfaces();
      builder.RegisterType<AccountRepository>().AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
