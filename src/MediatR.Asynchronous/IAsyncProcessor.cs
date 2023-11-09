namespace MediatR.Asynchronous
{
  public interface IAsyncProcessor
  {
    EventWaitHandle NewNotificationArrived { get; }
    void Start();
    void Stop();
    void WaitForIdle();
    void WaitForShutdown();
  }
}