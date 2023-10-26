using Autofac;
using Newtonsoft.Json;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

namespace SFC.Infrastructure.Features.Communication
{

  class AsyncEventProcessor : IEventAsyncProcessor
  {
    private readonly IOutbox _outbox;
    private readonly ILifetimeScope _container;
    private readonly IInbox _inbox;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private bool _shutdownCompleate;

    public AsyncEventProcessor(IOutbox outbox, ILifetimeScope container, IInbox inbox)
    {
      _outbox = outbox;
      _container = container;
      _inbox = inbox;
      _cancellationTokenSource = new CancellationTokenSource();
      _shutdownCompleate = false;
    }

    public void Start(string moduleName)
    {
      _shutdownCompleate = false;
      ThreadPool.QueueUserWorkItem(f => EventLoop(moduleName, _cancellationTokenSource.Token));
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    public void WaitForShutdown()
    {
      while (!_shutdownCompleate)
        Thread.Sleep(100);
    }

    private void EventLoop(string moduleName, CancellationToken token)
    {
      try
      {
        while (!token.IsCancellationRequested)
        {
          var lastProcessedId = _inbox.GetLastProcessedId(moduleName);
          var events = _outbox.Get(lastProcessedId, 100);
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
          Thread.Sleep(100);
        }
      }
      finally
      {
        _shutdownCompleate = true;
      }
    }
  }
}