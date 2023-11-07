namespace MediatR.Asynchronous
{
  public interface IMessagesAsyncProcessor
  {
    EventWaitHandle NewMessageArrived { get; }
    void Start(string moduleName);
    void Stop();
    void WaitForIdle();
    void WaitForShutdown();
  }
}