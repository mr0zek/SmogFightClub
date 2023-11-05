using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Transactions;

namespace MediatR.Asynchronous
{

  public class MessagesProcesor : IMessagesAsyncProcessor
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessagesProcessorStatusReporter _statusReporter;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly EventWaitHandle _shutDownCompleated;
    public EventWaitHandle NewMessageArrived { get; } = new EventWaitHandle(false, EventResetMode.ManualReset);

    public MessagesProcesor(
      IServiceProvider serviceProvider,
      IMessagesProcessorStatusReporter statusReporter)
    {
      _serviceProvider = serviceProvider;
      _statusReporter = statusReporter;
      _cancellationTokenSource = new CancellationTokenSource();
      _shutDownCompleated = new EventWaitHandle(false, EventResetMode.AutoReset);
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
        var outbox = _serviceProvider.GetService<IOutboxRepository>();
        _statusReporter.ReportStatus(MessagesProcesorStatus.Working);
        var inbox = _serviceProvider.GetService<IInbox>();
        while (!token.IsCancellationRequested)
        {
          var lastProcessedId = await inbox.GetLastProcessedId(moduleName);
          var messages = await outbox.Get(lastProcessedId, 100);
          while(!messages.Any())
          {
            if(token.IsCancellationRequested)
            {
              return;
            }
            _statusReporter.ReportStatus(MessagesProcesorStatus.Idle);
            NewMessageArrived.Reset();
            NewMessageArrived.WaitOne(1000);
            messages = await outbox.Get(lastProcessedId, 100);
          }
          _statusReporter.ReportStatus(MessagesProcesorStatus.Working);
          foreach (MessageData e in messages)
          {
            using(var scope = _serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
              await scope.ServiceProvider.GetService<IInbox>().SetProcessed(e.Id, moduleName);

              Type eventType = Type.GetType(e.Type);
              var @event = JsonSerializer.Deserialize(e.Data, eventType);

              if (e.MethodType == MethodType.Publish)
              {
                var publisher = scope.ServiceProvider.GetService<IPublisher>();
                await publisher.Publish(@event);
              }
              else
              {
                var sender = scope.ServiceProvider.GetService<ISender>();
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
  }
}