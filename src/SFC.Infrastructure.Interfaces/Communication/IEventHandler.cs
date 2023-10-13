namespace SFC.Infrastructure.Interfaces.Communication
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }

    public interface IEventHandlerAction<T>
    {
        void BeforeHandle(T @event, IEventHandler<T> handler);
        void AfterHandle();
    }
}