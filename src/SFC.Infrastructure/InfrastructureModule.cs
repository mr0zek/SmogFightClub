using Autofac;
using FluentValidation;
using Hangfire;
using SFC.Infrastructure.Features.Communication;
using SFC.Infrastructure.Features.Database;
using SFC.Infrastructure.Features.SmtpIntegration;
using SFC.Infrastructure.Features.TimeDependency;
using SFC.Infrastructure.Features.Tracing;
using SFC.Infrastructure.Features.Validation;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.Infrastructure
{
  [ModuleDefinition("Infastructure")]
  public class InfrastructureModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<IdentityProvider>().AsImplementedInterfaces();
      builder.RegisterType<CommandBus>().AsImplementedInterfaces();
      builder.RegisterType<EventBus>().AsImplementedInterfaces();
      builder.RegisterType<QueryBus>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterType<HangFireScheduler>().AsImplementedInterfaces();
      builder.RegisterType<FakeSmtpClient>().AsImplementedInterfaces();
      builder.RegisterType<TraceRepository>().AsImplementedInterfaces();
      builder.RegisterType<DatabaseMigrator>().AsImplementedInterfaces();
      builder.RegisterType<HandlerActivator>().AsSelf();
      builder.RegisterType<ContainerJobActivator>().As<JobActivator>();
      builder.RegisterType<CallStack>().InstancePerLifetimeScope().AsImplementedInterfaces();
      builder.RegisterType<OutboxRepository>().InstancePerLifetimeScope().AsImplementedInterfaces();
      builder.RegisterType<InboxRepository>().InstancePerLifetimeScope().AsImplementedInterfaces();
      builder.RegisterType<EventProcessor>().AsImplementedInterfaces();
      builder.RegisterType<EventBusWithAsync>().AsImplementedInterfaces();
      builder.RegisterAssemblyOpenGenericTypes(GetType().Assembly)
        .AsSelf()
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();      
    }
  }
}
