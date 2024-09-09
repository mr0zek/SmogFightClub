using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Text.Json;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace MediatR.Asynchronous
{

  public class AsyncProcesor : IAsyncProcessor
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly EventWaitHandle _shutDownCompleated;
    private readonly EventWaitHandle _idle;

    public EventWaitHandle NewNotificationArrived { get; } = new EventWaitHandle(false, EventResetMode.ManualReset);

    public AsyncProcesor(
      IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
      _cancellationTokenSource = new CancellationTokenSource();
      _shutDownCompleated = new EventWaitHandle(false, EventResetMode.AutoReset);
      _idle = new EventWaitHandle(false, EventResetMode.ManualReset);
    }

    public void Start()
    {
      ThreadPool.QueueUserWorkItem(f => EventLoop(_cancellationTokenSource.Token).Wait());
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    public void WaitForShutdown()
    {
      _shutDownCompleated.WaitOne();
    }

    private async Task EventLoop(CancellationToken token)
    {
      try
      {
        string moduleName = "internal";
        await using (var scope1 = _serviceProvider.CreateAsyncScope())
        {
          var dateTimeProvider = scope1.ServiceProvider.GetRequiredService<IDateTimeProvider>();
          var outbox = scope1.ServiceProvider.GetRequiredService<IOutboxRepository>();
          var inbox = scope1.ServiceProvider.GetRequiredService<IInboxRepository>();
          var statusReporter = scope1.ServiceProvider.GetRequiredService<IAsyncProcessorStatusReporter>();
          statusReporter.ReportStatus(AsyncProcesorStatus.Working);
          while (!token.IsCancellationRequested)
          {
            try
            {
              IEnumerable<MessageData> messages = await outbox.Get(100, moduleName);
              while (!messages.Any())
              {
                if (token.IsCancellationRequested)
                {
                  return;
                }
                statusReporter.ReportStatus(AsyncProcesorStatus.Idle);
                _idle.Set();
                NewNotificationArrived.Reset();
                NewNotificationArrived.WaitOne(50);
                _idle.Reset();
                messages = await outbox.Get(100, moduleName);
              }
              statusReporter.ReportStatus(AsyncProcesorStatus.Working);
              messages.AsParallel().ForAll(async e =>
              {
                var scopeFactory = scope1.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
                var txOptions = new TransactionOptions();
                txOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

                await using (var scope2 = scopeFactory.CreateAsyncScope())
                { 
                  Type eventType = Type.GetType(e.Type)!;
                  object @event = JsonSerializer.Deserialize(e.Data, eventType) ?? throw new NullReferenceException();

                  if (e.MethodType == MethodType.Publish)
                  {
                    var publisher = scope2.ServiceProvider.GetRequiredService<IPublisher>();
                    await publisher.Publish(@event);
                  }
                  else
                  {
                    var sender = scope2.ServiceProvider.GetRequiredService<ISender>();
                    await sender.Send(@event);
                  }
                }
              });
            }
            catch (DBConcurrencyException ex)
            {
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