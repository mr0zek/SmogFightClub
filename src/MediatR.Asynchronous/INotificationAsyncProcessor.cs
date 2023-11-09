namespace MediatR.Asynchronous
{
  public interface INotificationAsyncProcessor
  {
    EventWaitHandle NewNotificationArrived { get; }
    void Start(string moduleName);
    void Stop();
    void WaitForIdle();
    void WaitForShutdown();
  }
}