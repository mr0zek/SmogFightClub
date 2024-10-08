﻿using System;
using System.Linq;
using MediatR;
using MediatR.Asynchronous;
using MediatR.Pipeline;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddMediatRAsynchronous(this IServiceCollection services,
      Action<MediatRAsynchronousServiceConfiguration>? configuration = default)
  {
    var serviceConfig = new MediatRAsynchronousServiceConfiguration();

    configuration?.Invoke(serviceConfig);

    return services.AddMediatRAsynchronous(serviceConfig);
  }

  public static IServiceCollection AddMediatRAsynchronous(this IServiceCollection services,
      MediatRAsynchronousServiceConfiguration configuration)
  {
    services.TryAddScoped<IAsyncMediator, AsyncMediator>();
    services.TryAddScoped<IAsyncPublisher, AsyncMediator>();
    services.TryAddScoped<IAsyncSender, AsyncMediator>();
    services.TryAddScoped<IAsyncProcessor, AsyncProcesor>();
    services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.TryAddScoped<IAsyncProcessorStatusReporter, MessagesProcesorStatusReporter>();
    services.TryAddScoped(typeof(IInboxRepository), configuration.InboxRepository ?? throw new ArgumentException("Missing InboxRepository configuration"));
    services.TryAddScoped(typeof(IOutboxRepository), configuration.OutboxRepository ?? throw new ArgumentException("Missing OutboxRepository configuration"));

    return services;
  }
}