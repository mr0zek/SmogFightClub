namespace SFC.Infrastructure.Interfaces
{
  public interface IEventHandler<T>
  {
    void Handle(T @event);
  }
}