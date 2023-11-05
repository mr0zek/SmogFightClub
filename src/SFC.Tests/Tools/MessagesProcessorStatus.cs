using MediatR.Asynchronous;
using SFC.Infrastructure.Interfaces.Communication;
using System.Threading;

namespace SFC.Tests.Tools
{
  public class MessagesProcessorStatus : IMessagesProcessorStatusReporter
  {
    EventWaitHandle _waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

    public MessagesProcessorStatus()
    {
    }


    public void ReportStatus(MessagesProcesorStatus status)
    {
      if (status == MessagesProcesorStatus.Idle)
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