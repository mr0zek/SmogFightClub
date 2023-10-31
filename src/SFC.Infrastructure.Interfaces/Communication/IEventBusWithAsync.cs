namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventBusWithAsync 
  {
    Task Publish<T>(T @event) where T : IEvent;
  }
}
