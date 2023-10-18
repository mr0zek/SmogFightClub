namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventHandlerAction<T>
    where T : IEvent
  {
    void BeforeHandle(IEventExecutionContext<T> eventExecutionContext);
    void AfterHandle(IEventExecutionContext<T> eventExecutionContext);
  }
}