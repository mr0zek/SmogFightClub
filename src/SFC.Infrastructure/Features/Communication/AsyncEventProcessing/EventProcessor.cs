using Autofac;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Communication.AsyncEventProcessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{

  class EventProcessor : IEventAsyncProcessor
  {
    private readonly IOutbox _outbox;
    private readonly ILifetimeScope _container;
    private readonly IInbox _inbox;
    private readonly IEventProcessorStatusReporter _statusReporter;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly EventWaitHandle _shutDownCompleated;

    public EventProcessor(IOutbox outbox, ILifetimeScope container, IInbox inbox, IEventProcessorStatusReporter statusReporter)
    {
      _outbox = outbox;
      _container = container;
      _inbox = inbox;
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
      IPublisher publisher = _container.Resolve<IPublisher>();
      try
      {
        _statusReporter.ReportStatus(EventProcesorStatus.Working);
        while (!token.IsCancellationRequested)
        {
          var lastProcessedId = await _inbox.GetLastProcessedId(moduleName);
          var events = await _outbox.Get(lastProcessedId, 100);
          if (events.Any())
          {
            _statusReporter.ReportStatus(EventProcesorStatus.Working);
          }
          else
          {
            _statusReporter.ReportStatus(EventProcesorStatus.Idle);
            token.WaitHandle.WaitOne(100);
          }
          foreach (EventData e in events)
          {
            using (var scope = _container.BeginLifetimeScope())
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
              await _inbox.SetProcessed(e.Id, moduleName);

              Type eventType = Type.GetType(e.Type);
              var @event = JsonConvert.DeserializeObject(e.Data, eventType);

              await publisher.Publish(@event);

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