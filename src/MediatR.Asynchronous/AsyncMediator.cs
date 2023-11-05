using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediatR.Asynchronous
{
  public class AsyncMediator : IAsyncMediator
  {
    private readonly IOutboxRepository _outbox;
    private readonly IMessagesAsyncProcessor _eventAsyncProcessor;

    public AsyncMediator(IOutboxRepository outbox, IMessagesAsyncProcessor eventAsyncProcessor)
    {
      _outbox = outbox;
      _eventAsyncProcessor = eventAsyncProcessor;
    }

    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
      var data = JsonSerializer.Serialize(notification);
      var type = notification.GetType().AssemblyQualifiedName;
      await _outbox.Add(new MessageData() { Data = data, Type = type, MethodType = MethodType.Publish });
      _eventAsyncProcessor.NewMessageArrived.Set();
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
      await Publish((object)notification, cancellationToken);
    }

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
      await Send((object)request, cancellationToken);
    }

    public async Task Send(object request, CancellationToken cancellationToken = default)
    {
      var data = JsonSerializer.Serialize(request);
      var type = request.GetType().AssemblyQualifiedName;
      await _outbox.Add(new MessageData() { Data = data, Type = type, MethodType = MethodType.Send });
      _eventAsyncProcessor.NewMessageArrived.Set();
    }
  }
}
