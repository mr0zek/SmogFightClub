namespace SFC.Infrastructure.Interfaces.Communication
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
