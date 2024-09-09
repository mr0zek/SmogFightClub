using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.NotificationPipeline
{
  public delegate Task EventHandlerDelegate();

  public interface INotificationPipelineBehavior<TNotification> where TNotification : INotification
  {
    Task Handle(
      TNotification notification,
      EventHandlerDelegate next,
      INotificationHandler<TNotification> handler,
      CancellationToken cancellationToken);
  }
}