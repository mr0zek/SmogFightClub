
namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventAsyncProcessor
  {
    void Start(string moduleName);
    void Stop();
    void WaitForShutdown();
  }
}