using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public delegate Task EventHandlerDelegate();

  public interface INotificationPipelineBehavior<TEvent> where TEvent : IEvent
  {
    Task Handle(TEvent notification, EventHandlerDelegate next, CancellationToken cancellationToken);
  }
}