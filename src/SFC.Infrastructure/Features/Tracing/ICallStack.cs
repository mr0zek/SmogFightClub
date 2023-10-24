using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  interface ICallStack
  {
    void StartCall(string moduleName, string callName, string type, string callingModuleName = null);
    void FinishCall(string callName);
  }
}
