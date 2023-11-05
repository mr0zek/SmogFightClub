using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.Communication
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