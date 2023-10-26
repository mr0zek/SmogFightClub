using Autofac;
using Newtonsoft.Json;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;

namespace SFC.Infrastructure.Features.Communication
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
      ThreadPool.QueueUserWorkItem(f => EventLoop(moduleName, _cancellationTokenSource.Token));
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    public void WaitForShutdown()
    {
      _shutDownCompleated.WaitOne();
    }

    private void EventLoop(string moduleName, CancellationToken token)
    {
      try
      {
        _statusReporter.ReportStatus(EventProcesorStatus.Working);
        while (!token.IsCancellationRequested)
        {
          var lastProcessedId = _inbox.GetLastProcessedId(moduleName);
          var events = _outbox.Get(lastProcessedId, 100);
          if(events.Any())
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
            if (token.IsCancellationRequested)
            {
              break;
            }
            using (var scope = _container.BeginLifetimeScope())
            using (TransactionScope ts = new TransactionScope())
            {
              _inbox.SetProcessed(e.Id, moduleName);

              Type eventType = Type.GetType(e.Type);
              var @event = JsonConvert.DeserializeObject(e.Data, eventType);
              var handlerType = typeof(IEventHandler<>);
              handlerType = handlerType.MakeGenericType(eventType);
              var handlersType = typeof(IEnumerable<>).MakeGenericType(handlerType);
              var handlers = (IEnumerable)scope.Resolve(handlersType);
              var executionContextType = typeof(EventExecutionContext<>).MakeGenericType(eventType);
              var actionsType = typeof(IEnumerable<>).MakeGenericType(typeof(IEventHandlerAction<>).MakeGenericType(eventType));
              var actions = (IEnumerable)scope.Resolve(actionsType);


              foreach (var handler in handlers)
              {
                if (token.IsCancellationRequested)
                {
                  return;
                }
                var executionContext = scope.Resolve(executionContextType);
                executionContextType.InvokeMember("Event", System.Reflection.BindingFlags.SetProperty, null, executionContext, new object?[] { @event });
                executionContextType.InvokeMember("Handler", System.Reflection.BindingFlags.SetProperty, null, executionContext, new object?[] { handler });


                foreach (var action in actions)
                {
                  try
                  {
                    action.GetType().InvokeMember("BeforeHandle", System.Reflection.BindingFlags.InvokeMethod, null, action, new object?[] { executionContext });
                  }
                  catch (Exception ex)
                  {
                    Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
                  }
                }

                try
                {
                  handlerType.InvokeMember("Handle", System.Reflection.BindingFlags.InvokeMethod, null, handler, new object?[] { @event });
                }
                catch (Exception ex)
                {
                  executionContext.GetType().InvokeMember("Exception", System.Reflection.BindingFlags.SetProperty, null, executionContext, new object?[] { ex });
                }

                foreach (var action in actions)
                {
                  try
                  {
                    action.GetType().InvokeMember("AfterHandle", System.Reflection.BindingFlags.InvokeMethod, null, action, new object?[] { executionContext });
                  }
                  catch (Exception ex)
                  {
                    Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
                  }
                }
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