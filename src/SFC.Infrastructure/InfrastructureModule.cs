﻿using Autofac;
using Hangfire;
using MediatR;
using MediatR.Asynchronous;
using MediatR.Asynchronous.MsSql;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using SFC.Infrastructure.Features.Communication;
using SFC.Infrastructure.Features.Database;
using SFC.Infrastructure.Features.SmtpIntegration;
using SFC.Infrastructure.Features.TimeDependency;
using SFC.Infrastructure.Features.Tracing;
using SFC.Infrastructure.Features.Validation;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Infrastructure.Interfaces.Modules;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR.NotificationPipeline;

namespace SFC.Infrastructure
{
  [ModuleDefinition("Infastructure")]
  public class InfrastructureModule : IHaveAutofacRegistrations, IModule, IHaveAspConfiguration, IHaveWorker
  {
    IAsyncProcessor? _eventAsyncProcessor;
    public void StartWorker(IComponentContext container)
    {
      _eventAsyncProcessor = container.Resolve<IAsyncProcessor>();
      _eventAsyncProcessor.Start();
    }

    public void StopWorker()
    {
      _eventAsyncProcessor?.Stop();
    }

    public void WaitForShutdown()
    {
      _eventAsyncProcessor?.WaitForShutdown();
    }

    public void Configure(WebApplicationBuilder builder)
    {
      builder.Services.AddMediatRAsynchronous(cfg =>
      {
        cfg.OutboxRepository = sp => new OutboxRepository(sp.GetRequiredService<ConnectionString>().ToString());
        cfg.InboxRepository = sp => new InboxRepository(sp.GetRequiredService<ConnectionString>().ToString());
      });
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
      builder.RegisterType<TraceRepository>().AsImplementedInterfaces();
      builder.RegisterType<DatabaseMigrator>().AsImplementedInterfaces();
      builder.RegisterType<MessagesProcesorStatusReporter>().AsImplementedInterfaces();
      builder.RegisterType<CommandBus>().AsImplementedInterfaces();
      builder.RegisterType<EventBus>().AsImplementedInterfaces();
      builder.RegisterType<QueryBus>().AsImplementedInterfaces();
      builder.RegisterType<HandlerActivator>().AsSelf();
      builder.RegisterType<ContainerJobActivator>().As<JobActivator>();
      builder.RegisterType<CallStack>().InstancePerLifetimeScope().AsImplementedInterfaces();
      builder.RegisterType<AsyncProcesor>().AsImplementedInterfaces().SingleInstance();
      builder.RegisterType<AsyncMediator>().AsImplementedInterfaces();
      builder.RegisterGeneric(typeof(TraceHandlerBehavior<,>)).AsImplementedInterfaces();
      builder.RegisterGeneric(typeof(ValidationBehavior<,>)).AsImplementedInterfaces();
      builder.RegisterType<ValidationExceptionHandlingMiddleware>().AsImplementedInterfaces();
      builder.RegisterType<ValidationExceptionHandlingMiddleware>().AsImplementedInterfaces();
      builder.RegisterGenericDecorator(typeof(NotificationPipelineDecorator<>), typeof(INotificationHandler<>));
      builder.RegisterGeneric(typeof(TraceEventHandlerBehavior<>)).AsImplementedInterfaces();
    }

    private void RegisterMediator(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

      builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
      builder.RegisterType<MediatR.Asynchronous.DateTimeProvider>().AsImplementedInterfaces();
    }
  }
}
