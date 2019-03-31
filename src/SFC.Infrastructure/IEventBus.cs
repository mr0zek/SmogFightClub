namespace SFC.Infrastructure
{
  public interface IEventBus
  {
    void Publish<T>(T @event);
  }
}
