using SFC.Infrastructure.Interfaces.Communication;
using System.Threading;

namespace SFC.Tests.Tools
{
  public class EventProcessorStatus : IEventProcessorStatusReporter
  {
    EventWaitHandle _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

    public EventProcessorStatus()
    {
    }


    public void ReportStatus(EventProcesorStatus status)
    {
      if (status == EventProcesorStatus.Idle)
      {
        _waitHandle.Set();
      }
    }

    public void WaitIlde() 
    {
      _waitHandle.WaitOne();
    }
  }
}