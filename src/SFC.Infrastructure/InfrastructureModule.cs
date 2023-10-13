using Autofac;
using FluentValidation;
using SFC.Infrastructure.Features.Communication;
using SFC.Infrastructure.Features.SmtpIntegration;
using SFC.Infrastructure.Features.TimeDependency;
using SFC.Infrastructure.Features.Tracing;
using SFC.Infrastructure.Features.Validation;
using SFC.Infrastructure.Interfaces;

namespace SFC.Infrastructure
{
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
      builder.RegisterType<HttpExecutionContext>().InstancePerLifetimeScope().AsImplementedInterfaces();
      
      builder.RegisterAssemblyOpenGenericTypes(GetType().Assembly)
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();      
    }
  }
}
