using MediatR.Asynchronous;
using SFC.Infrastructure.Interfaces.Communication;
using System.Threading;

namespace SFC.Tests.Tools
{
  public class MessagesProcessorStatus : IAsyncProcessorStatusReporter
  {
    EventWaitHandle _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

    public MessagesProcessorStatus()
    {
    }


    public void ReportStatus(AsyncProcesorStatus status)
    {
      if (status == AsyncProcesorStatus.Idle)
      {
        _waitHandle.Set();
      }
    }

    public void WaitIlde() 
    {      
      _waitHandle.Reset();      
      _waitHandle.WaitOne();
    }
  }
}