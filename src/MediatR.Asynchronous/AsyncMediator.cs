using Microsoft.Extensions.DependencyInjection;
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
    private readonly IEnumerable<INotificationAsyncProcessor> _eventAsyncProcessor;

    public AsyncMediator(IServiceProvider serviceProvider)
    {
      _outbox = serviceProvider.GetRequiredService<IOutboxRepository>();
      _eventAsyncProcessor = serviceProvider.GetRequiredService<IEnumerable<INotificationAsyncProcessor>>();
    }

    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
      var data = JsonSerializer.Serialize(notification);
      string type = notification.GetType().AssemblyQualifiedName ?? throw new NullReferenceException();
      await _outbox.Add(new MessageData(0, data, type, MethodType.Publish));
      _eventAsyncProcessor.All(f=> f.NewNotificationArrived.Set());
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
      string data = JsonSerializer.Serialize(request);
      string type = request.GetType().AssemblyQualifiedName ?? throw new NullReferenceException();
      await _outbox.Add(new MessageData(0, data, type, MethodType.Send));
      _eventAsyncProcessor.All(f => f.NewNotificationArrived.Set());
    }
  }
}
