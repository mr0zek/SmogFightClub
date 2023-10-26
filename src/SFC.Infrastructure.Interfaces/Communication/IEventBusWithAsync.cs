namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventBusWithAsync 
  {
    void Publish<T>(T @event) where T : IEvent;
  }
}
