namespace SFC.Infrastructure.Interfaces
{
  public interface IEventBus
  {
    void Publish<T>(T @event);
  }
}
