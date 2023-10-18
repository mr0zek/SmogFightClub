namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventHandler<T>
    where T : IEvent
  {
    void Handle(T @event);
  }
}