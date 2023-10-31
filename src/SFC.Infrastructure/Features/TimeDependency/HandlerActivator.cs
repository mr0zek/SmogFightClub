using Autofac;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class HandlerActivator
  {
    private readonly ILifetimeScope _container;
    private readonly IDateTimeProvider _dateTimeProvider;

    public HandlerActivator(ILifetimeScope container, IDateTimeProvider dateTimeProvider)
    {
      _container = container;
      _dateTimeProvider = dateTimeProvider;
    }

    public void Run(Type type)
    {
      CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      using(var ts = new TransactionScope())
      using (var scope = _container.BeginLifetimeScope())
      {
        var handler = (INotificationHandler<TimeEvent>)scope.Resolve(type);

        TimeEvent @event = new TimeEvent(_dateTimeProvider.Now());

        handler.Handle(@event, cancellationTokenSource.Token).Wait(cancellationTokenSource.Token);               

        ts.Complete();
      }
    }
  }
}
