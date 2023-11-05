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
  class NotificationPipelineDecorator<TNotification> : INotificationHandler<TNotification>
  where TNotification : INotification
  {
    private readonly INotificationHandler<TNotification> _notificationHandler;
    private readonly IEnumerable<INotificationPipelineBehavior<TNotification>> _pipelineBehaviors;

    public NotificationPipelineDecorator(
      INotificationHandler<TNotification> notificationHandler,
      IEnumerable<INotificationPipelineBehavior<TNotification>> pipelineBehaviors)
    {
      _notificationHandler = notificationHandler;
      _pipelineBehaviors = pipelineBehaviors;
    }

    public Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
      async Task<Unit> Handler()
      {
        await _notificationHandler.Handle(notification, cancellationToken);

        return Unit.Value;
      }

      return _pipelineBehaviors
        .Reverse()
        .Aggregate(
          (EventHandlerDelegate)Handler,
          (next, pipeline) => () => pipeline.Handle(notification, next, _notificationHandler, cancellationToken))();
    }
  }
}
