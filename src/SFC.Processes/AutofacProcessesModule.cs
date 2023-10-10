using Autofac;
using FluentValidation;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistration;
using SFC.Processes.Features.UserRegistrationSaga;

namespace SFC.Processes
{
  public class AutofacProcessesModule : Module
  {
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
