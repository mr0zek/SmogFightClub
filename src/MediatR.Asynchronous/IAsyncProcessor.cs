namespace MediatR.Asynchronous
{
  public interface IAsyncProcessor
  {
    EventWaitHandle NewNotificationArrived { get; }
    void Start(string moduleName);
    void Stop();
    void WaitForIdle();
    void WaitForShutdown();
  }
}