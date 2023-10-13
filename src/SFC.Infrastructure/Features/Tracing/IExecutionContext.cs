using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  public interface IExecutionContext
  {
    void StartCall(string moduleName, string callName);
    void FinishCall();
  }
}
