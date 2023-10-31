namespace SFC.Infrastructure.Interfaces.Communication.AsyncEventProcessing
{
    public interface IEventAsyncProcessor
    {
        void Start(string moduleName);
        void Stop();
        void WaitForShutdown();
    }
}