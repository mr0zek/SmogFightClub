﻿using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Transactions;

namespace MediatR.Asynchronous
{

  public class AsyncProcesor : IAsyncProcessor
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly IAsyncProcessorStatusReporter _statusReporter;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly EventWaitHandle _shutDownCompleated;
    private readonly EventWaitHandle _idle;

    public EventWaitHandle NewNotificationArrived { get; } = new EventWaitHandle(false, EventResetMode.ManualReset);

    public AsyncProcesor(
      IServiceProvider serviceProvider,
      IAsyncProcessorStatusReporter statusReporter)
    {
      _serviceProvider = serviceProvider;
      _statusReporter = statusReporter;
      _cancellationTokenSource = new CancellationTokenSource();
      _shutDownCompleated = new EventWaitHandle(false, EventResetMode.AutoReset);
      _idle = new EventWaitHandle(false, EventResetMode.ManualReset);
    }

    public void Start(string moduleName)
    {
      ThreadPool.QueueUserWorkItem(f => EventLoop(moduleName, _cancellationTokenSource.Token).Wait());
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    public void WaitForShutdown()
    {
      _shutDownCompleated.WaitOne();
    }

    private async Task EventLoop(string moduleName, CancellationToken token)
    {
      try
      {
        IOutboxRepository outbox = _serviceProvider.GetRequiredService<IOutboxRepository>();
        _statusReporter.ReportStatus(AsyncProcesorStatus.Working);
        var inbox = _serviceProvider.GetRequiredService<IInboxRepository>();
        while (!token.IsCancellationRequested)
        {
          var lastProcessedId = await inbox.GetLastProcessedId(moduleName);
          IEnumerable<MessageData> messages = await outbox.Get(lastProcessedId, 100);
          while(!messages.Any())
          {
            if(token.IsCancellationRequested)
            {
              return;
            }
            _statusReporter.ReportStatus(AsyncProcesorStatus.Idle);
            _idle.Set();
            NewNotificationArrived.Reset();
            NewNotificationArrived.WaitOne(1000);
            _idle.Reset();
            messages = await outbox.Get(lastProcessedId, 100);
          }          
          _statusReporter.ReportStatus(AsyncProcesorStatus.Working);
          foreach (MessageData e in messages)
          {
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
              await (scope.ServiceProvider.GetRequiredService<IInboxRepository>())
                .SetProcessed(e.Id, moduleName);

              Type eventType = Type.GetType(e.Type)!;
              object @event = JsonSerializer.Deserialize(e.Data, eventType) ?? throw new NullReferenceException();

              if (e.MethodType == MethodType.Publish)
              {
                var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(@event);
              }
              else
              {
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                await sender.Send(@event);
              }

              ts.Complete();
            }
          }
        }
      }
      finally
      {
        _shutDownCompleated.Set();
      }
    }

    public void WaitForIdle()
    {
      _idle.WaitOne();      
    }
  }
}