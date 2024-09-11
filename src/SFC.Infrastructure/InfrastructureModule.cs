using Autofac;
using Hangfire;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using SFC.Infrastructure.Features.Communication;
using SFC.Infrastructure.Features.Database;
using SFC.Infrastructure.Features.SmtpIntegration;
using SFC.Infrastructure.Features.TimeDependency;
using SFC.Infrastructure.Features.Validation;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Modules;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SFC.Infrastructure
{
  public class InfrastructureModule : IHaveAutofacRegistrations, IModule, IHaveAspConfiguration
  {   
    public void Configure(WebApplicationBuilder builder)
    {
    }

    public void Configure(WebApplication app)
    {
      app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
    }

    public void RegisterTypes(ContainerBuilder builder)
    {
      RegisterMediator(builder);
      builder.RegisterType<IdentityProvider>().AsImplementedInterfaces();      
      builder.RegisterType<Features.TimeDependency.DateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterType<HangFireScheduler>().AsImplementedInterfaces();
      builder.RegisterType<FakeSmtpClient>().AsImplementedInterfaces();
      builder.RegisterType<DatabaseMigrator>().AsImplementedInterfaces();
      builder.RegisterType<CommandBus>().AsImplementedInterfaces();
      builder.RegisterType<EventBus>().AsImplementedInterfaces();
      builder.RegisterType<QueryBus>().AsImplementedInterfaces();
      builder.RegisterType<HandlerActivator>().AsSelf();
      builder.RegisterType<ContainerJobActivator>().As<JobActivator>();
      builder.RegisterGeneric(typeof(ValidationBehavior<,>)).AsImplementedInterfaces();
      builder.RegisterType<ValidationExceptionHandlingMiddleware>().AsImplementedInterfaces();
      builder.RegisterType<ValidationExceptionHandlingMiddleware>().AsImplementedInterfaces();
    }

    private void RegisterMediator(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

      builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }
  }
}
