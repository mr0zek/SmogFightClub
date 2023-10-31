using MediatR;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventHandler<T> : INotificationHandler<T>
    where T : IEvent
  {    
  }
}