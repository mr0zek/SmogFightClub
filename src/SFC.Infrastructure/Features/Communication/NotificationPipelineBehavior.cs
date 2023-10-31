using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication
{
  internal class NotificationPipelineDecorator<TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
  {
    private readonly INotificationHandler<TEvent> _notificationHandler;
    private readonly IEnumerable<INotificationPipelineBehavior<TEvent>> _pipelineBehaviors;

    public NotificationPipelineDecorator(
      INotificationHandler<TEvent> notificationHandler, 
      IEnumerable<INotificationPipelineBehavior<TEvent>> pipelineBehaviors) 
    {
      _notificationHandler = notificationHandler;
      _pipelineBehaviors = pipelineBehaviors;
    }

    public Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
      async Task<Unit> Handler()
      {
        await _notificationHandler.Handle(notification, cancellationToken);

        return Unit.Value;
      }

      return _pipelineBehaviors
        .Reverse()
        .Aggregate(
          (EventHandlerDelegate) Handler, 
          (next, pipeline) => () => pipeline.Handle(notification, next, cancellationToken))();      
    }
  }
}
