using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  interface ICallStack
  {
    Task StartCall(string moduleName, string callName, string type, string callingModuleName = null);
    Task FinishCall(string callName);
  }
}
