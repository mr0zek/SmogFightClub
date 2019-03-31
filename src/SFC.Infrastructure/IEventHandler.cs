namespace SFC.Infrastructure
{
  public interface IEventHandler<T>
  {
    void Handle(T @event);
  }
}